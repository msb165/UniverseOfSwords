using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class TrueRuneBlade : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.TrueRuneBlade>().Texture;

        public float SwingDirection => Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 1;
            Projectile.noEnchantmentVisuals = true;
        }

        Player Player => Main.player[Projectile.owner];

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.ai[0] <= 0f)
            {
                SoundEngine.PlaySound(SoundID.Item60, Projectile.position);
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] < 32f)
            {
                Projectile.Center = Player.RotatedRelativePoint(Player.Center);
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4 + MathHelper.SmoothStep(-2f * SwingDirection * Player.direction, 2f * SwingDirection * Player.direction, Projectile.ai[0] * 0.03f);
                Projectile.scale = 1.5f - MathHelper.SmoothStep(0f, 1.25f, Projectile.ai[0] * 0.0125f);

                if (Projectile.ai[0] % 29f == 0f && Main.myPlayer == Projectile.owner)
                {
                    Vector2 newVel = Vector2.Normalize(Main.MouseWorld - Player.Center).RotatedByRandom(MathHelper.ToRadians(5f)) * 16f;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Player.Center + newVel, newVel, ModContent.ProjectileType<TrueRuneWave>(), Projectile.damage, Projectile.knockBack, Projectile.owner, ai1: Main.rand.Next(0, 5), ai2: Main.rand.NextFloat() % 1f);
                }
            }

            if (!Player.channel && Player.noItems && Player.CCed || Projectile.ai[0] > 32f || Projectile.ai[0] >= 32f && !Player.channel)
            {
                Projectile.Kill();
                Player.reuseDelay = 2;
            }

            SetPlayerValues();
            for (int i = 0; i < 5; i++)
            {
                if (Main.rand.NextBool(2))
                {
                    Vector2 dustRot = (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 90f;
                    Vector2 dustVel = Vector2.Normalize(dustRot).RotatedBy(MathHelper.Pi + MathHelper.PiOver2 * -Player.direction * SwingDirection);
                    Dust dust = Dust.NewDustPerfect(Vector2.Lerp(Player.Center + dustRot * 0.5f, Player.Center + dustRot * Projectile.scale, 0.25f * i), Utils.SelectRandom<int>(Main.rand, [DustID.IceTorch, DustID.OrangeTorch, DustID.CursedTorch]), dustVel, Scale: Main.rand.NextFloat(1f, 2f));
                    dust.noGravity = true;
                }
            }

            if (Main.myPlayer == Projectile.owner)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.whoAmI != Projectile.whoAmI && Projectile.Colliding(Projectile.Hitbox, proj.Hitbox) && !proj.reflected && proj.hostile && Main.rand.Next(1, 100) <= Player.HeldItem.GetGlobalItem<ReflectionChance>().reflectChance)
                    {
                        SoundEngine.PlaySound(SoundID.Item150, Projectile.Center);
                        proj.velocity = -proj.oldVelocity;
                        proj.friendly = true;
                        proj.hostile = false;
                        proj.reflected = true;
                    }
                }
            }
        }

        public void SetPlayerValues()
        {
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            target.AddBuff(BuffID.Ichor, 360);
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.OnFire, 360);
            target.AddBuff(BuffID.CursedInferno, 360);

            int healingAmt = hit.Damage / 15;
            Player.statLife += healingAmt;
            Player.Heal(healingAmt);
            NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Player.Center, Player.Center + (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 90f * Projectile.scale, 2f, ref _);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = new(0f, texture.Height);
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.None;
            Color trailColor = Color.White with { A = 80 };

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.8f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
