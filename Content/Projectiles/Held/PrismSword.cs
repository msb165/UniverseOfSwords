using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Weapons;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Held
{
    internal class PrismSword : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.PrismSword>().Texture;

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 17;
            Projectile.extraUpdates = 1;
            Projectile.noEnchantmentVisuals = true;
        }

        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            float num37 = 30f;
            if (Projectile.ai[0] > 90f)
            {
                num37 = 15f;
            }
            if (Projectile.ai[0] > 120f)
            {
                num37 = 5f;
            }
            Projectile.damage = (int)(float)(Player.GetTotalDamage(DamageClass.Melee).ApplyTo(Player.HeldItem.damage));
            Projectile.ai[0]++;
            Projectile.ai[1]++;
            int num38 = 10;
            bool flag9 = false;
            if (Projectile.ai[0] % num37 == 0f)
            {
                flag9 = true;
            }
            if (Projectile.ai[1] >= 1f)
            {
                Projectile.ai[1] = 0f;
                flag9 = true;
                if (Main.myPlayer == Projectile.owner)
                {
                    float speed = Player.HeldItem.shootSpeed * Projectile.scale;
                    Vector2 vector18 = Player.RotatedRelativePoint(Player.MountedCenter);
                    Vector2 value6 = Main.MouseWorld - vector18;
                    Vector2 value7 = value6;
                    value7 = value7.SafeNormalize(-Vector2.UnitY);
                    value7 = Vector2.Normalize(Vector2.Lerp(value7, Vector2.Normalize(Projectile.velocity), 0.92f));
                    value7 *= speed;
                    if (value7.X != Projectile.velocity.X || value7.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.rotation = value6.ToRotation() + MathHelper.Lerp(-0.75f, 0.75f, MathF.Sin(Projectile.ai[0] * 0.1f)) + MathHelper.PiOver4;
                    Projectile.velocity = Projectile.rotation.ToRotationVector2();
                }
            }
            if (Projectile.soundDelay <= 0)
            {
                Projectile.soundDelay = num38;
                Projectile.soundDelay *= 4;
                if (Projectile.ai[0] != 1f)
                {
                    SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
                }
            }
            if (flag9 && Main.myPlayer == Projectile.owner)
            {
                if (Player.channel && !Player.noItems && !Player.CCed)
                {
                    if (Projectile.ai[0] == 1f)
                    {
                        Vector2 spawnPos = Projectile.Center;
                        Vector2 spawnVel = Projectile.velocity;
                        spawnVel = spawnVel.SafeNormalize(-Vector2.UnitY);
                        int damage = Projectile.damage;
                        for (int m = 0; m < 6; m++)
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnPos, spawnVel * 32f, ModContent.ProjectileType<PrismLaser>(), damage, Projectile.knockBack, Projectile.owner, m, Projectile.whoAmI);
                        }
                        Projectile.netUpdate = true;
                    }
                }
                else
                {
                    Projectile.Kill();
                }
            }
            Projectile.Center = Player.Center;
            SetPlayerValues();
        }

        public void SetPlayerValues()
        {
            Player.ChangeDir(Projectile.direction);
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            float rotation = Projectile.rotation - MathHelper.PiOver2;
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation);
        }


        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.rotation.ToRotationVector2() * 60f, 22f * Projectile.scale, ref collisionPoint);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 drawOrigin = new(texture.Width, texture.Height);
            Color drawColor = Color.White * Projectile.Opacity;
            Color trailColor = drawColor with { A = 0 };

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.91f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor * 0.5f, Projectile.oldRot[i], drawOrigin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation + MathHelper.PiOver2 + MathHelper.PiOver4, drawOrigin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
