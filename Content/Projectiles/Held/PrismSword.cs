using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Weapons;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
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
            float time = 30f;
            if (Projectile.ai[0] > 90f)
            {
                time = 15f;
            }
            if (Projectile.ai[0] > 120f)
            {
                time = 5f;
            }
            Projectile.damage = (int)(float)(Player.GetTotalDamage(DamageClass.Melee).ApplyTo(Player.HeldItem.damage));
            Projectile.ai[0]++;
            Projectile.ai[1]++;
            int soundDelayAmount = 10;
            bool shouldSpawn = false;
            if (Projectile.ai[0] % time == 0f)
            {
                shouldSpawn = true;
            }
            if (Projectile.ai[1] >= 1f)
            {
                Projectile.ai[1] = 0f;
                shouldSpawn = true;
                if (Main.myPlayer == Projectile.owner)
                {
                    float speed = Player.HeldItem.shootSpeed * Projectile.scale;
                    Vector2 position = Player.RotatedRelativePoint(Player.MountedCenter);
                    Vector2 velocity = Main.MouseWorld - position;
                    Vector2 alteredVelocity = velocity;
                    alteredVelocity = alteredVelocity.SafeNormalize(-Vector2.UnitY);
                    alteredVelocity = Vector2.Normalize(Vector2.Lerp(alteredVelocity, Vector2.Normalize(Projectile.velocity), 0.92f));
                    alteredVelocity *= speed;
                    if (alteredVelocity.X != Projectile.velocity.X || alteredVelocity.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.rotation = velocity.ToRotation() + MathHelper.Lerp(-0.75f, 0.75f, MathF.Sin(Projectile.ai[0] * 0.1f)) + MathHelper.PiOver4;
                    Projectile.velocity = Projectile.rotation.ToRotationVector2();
                }
            }
            if (Projectile.soundDelay <= 0)
            {
                Projectile.soundDelay = soundDelayAmount;
                Projectile.soundDelay *= 4;
                if (Projectile.ai[0] != 1f)
                {
                    SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
                }
            }
            if (shouldSpawn && Main.myPlayer == Projectile.owner)
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target) && Main.myPlayer == Projectile.owner)
            {
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
            }
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
