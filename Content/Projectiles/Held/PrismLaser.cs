using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Projectiles.Held
{
    public class PrismLaser : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.LastPrismLaser}";

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 17;
        }

        public Projectile LaserShooter => Main.projectile[(int)Projectile.ai[1]];

        public override void AI()
        {
            Vector2? vector59 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }

            if (!LaserShooter.active || LaserShooter.type != ModContent.ProjectileType<PrismSword>())
            {
                Projectile.Kill();
                return;
            }
            float num705 = (int)Projectile.ai[0] - 2.5f;
            Vector2 laserVelocity = Vector2.Normalize(LaserShooter.velocity) * 6f;
            Projectile prismSword = LaserShooter;
            float num706 = num705 * MathHelper.Pi / 6f;
            float num708 = 1f;
            float num709 = 15f;
            float num710 = -2f;
            float num707;
            if (prismSword.ai[0] < 180f)
            {
                num708 = 1f - prismSword.ai[0] / 180f;
                num709 = 20f - prismSword.ai[0] / 180f * 14f;
                if (prismSword.ai[0] < 120f)
                {
                    num707 = 20f - 4f * (prismSword.ai[0] / 120f);
                    Projectile.Opacity = prismSword.ai[0] / 120f * 0.4f;
                }
                else
                {
                    num707 = 16f - 10f * ((prismSword.ai[0] - 120f) / 60f);
                    Projectile.Opacity = 0.4f + (prismSword.ai[0] - 120f) / 60f * 0.6f;
                }
                num710 = -22f + prismSword.ai[0] / 180f * 20f;
            }
            else
            {
                num708 = 0f;
                num707 = 1.75f;
                num709 = 6f;
                Projectile.Opacity = 1f;
                num710 = -2f;
            }
            float num711 = (prismSword.ai[0] + num705 * num707) / (num707 * 6f) * (MathHelper.TwoPi);
            num706 = Vector2.UnitY.RotatedBy(num711).Y * (MathHelper.Pi / 6f) * num708;
            Vector2 zero2 = (Vector2.UnitY.RotatedBy(num711) * new Vector2(4f, num709)).RotatedBy(prismSword.velocity.ToRotation());
            Projectile.position = prismSword.Center + laserVelocity * 16f - Projectile.Size / 2f + new Vector2(0f, -LaserShooter.gfxOffY);
            Projectile.position += prismSword.velocity.ToRotation().ToRotationVector2() * num710;
            Projectile.position += zero2;
            Projectile.velocity = Vector2.Normalize(prismSword.velocity).RotatedBy(num706 + MathHelper.PiOver2);
            Projectile.scale = 1.4f * (1f - num708);
            Projectile.damage = prismSword.damage;
            if (prismSword.ai[0] >= 180f)
            {
                Projectile.damage *= 3;
                vector59 = prismSword.Center;
            }
            if (!Collision.CanHitLine(Main.player[Projectile.owner].Center, 0, 0, prismSword.Center, 0, 0))
            {
                vector59 = Main.player[Projectile.owner].Center;
            }
            Projectile.friendly = prismSword.ai[0] > 30f;

            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            Projectile.velocity = Projectile.rotation.ToRotationVector2();
            float num717 = 0f;
            Vector2 samplingPoint = Projectile.Center;
            if (vector59.HasValue)
            {
                samplingPoint = vector59.Value;
            }
            float[] samplePoints = new float[2];
            Collision.LaserScan(samplingPoint, Projectile.velocity, num717 * Projectile.scale, 384f, samplePoints);
            float samples = 0f;
            for (int i = 0; i < samplePoints.Length; i++)
            {
                samples += samplePoints[i];
            }
            samples /= 2f;
            float amount = 0.75f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], samples, amount);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], 22f * Projectile.scale, ref collisionPoint);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (!(Projectile.velocity == Vector2.Zero))
            {
                Texture2D value67 = TextureAssets.Projectile[Type].Value;
                float num270 = Projectile.localAI[1];
                float laserLuminance = 0.5f;
                float laserAlphaMultiplier = 0f;
                Color laserColor = Main.hslToRgb(Projectile.GetLastPrismHue(Projectile.ai[0], ref laserLuminance, ref laserAlphaMultiplier), 1f, laserLuminance);
                laserColor.A = (byte)(laserColor.A * laserAlphaMultiplier);
                Vector2 centerFloored = Projectile.Center.Floor();
                centerFloored += Projectile.velocity * Projectile.scale * 10.5f;
                num270 -= Projectile.scale * 14.5f * Projectile.scale;
                Vector2 scale = new(Projectile.scale);
                DelegateMethods.f_1 = 1f;
                DelegateMethods.c_1 = laserColor * 0.75f * Projectile.Opacity;
                Utils.DrawLaser(Main.spriteBatch, value67, centerFloored - Main.screenPosition, centerFloored + Projectile.velocity * num270 - Main.screenPosition, scale, DelegateMethods.RainbowLaserDraw);
                DelegateMethods.c_1 = new Color(255, 255, 255, 127) * 0.75f * Projectile.Opacity;
                Utils.DrawLaser(Main.spriteBatch, value67, centerFloored - Main.screenPosition, centerFloored + Projectile.velocity * num270 - Main.screenPosition, scale / 2f, DelegateMethods.RainbowLaserDraw);
            }
            return false;
        }
    }
}
