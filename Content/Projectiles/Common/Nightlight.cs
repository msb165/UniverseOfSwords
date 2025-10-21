using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class Nightlight : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(18);
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.velocity.Length() > 4f)
            {
                Projectile.velocity *= 0.94f;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.VampireKnivesAI(ai: 0, 30f);

            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.VilePowder);
                dust.velocity *= 0.3f;
                dust.position = Projectile.Center - Projectile.velocity + Utils.RandomVector2(Main.rand, -Projectile.width / 2 + 4, Projectile.width / 2- 4);
                dust.alpha = Projectile.alpha;
                dust.noGravity = true;
                dust.scale = 1f;

                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PinkTorch);
                dust2.velocity *= 0.3f;
                dust2.position = Projectile.Center - Projectile.velocity + Utils.RandomVector2(Main.rand, -Projectile.width / 2 + 4, Projectile.width / 2 - 4);
                dust2.alpha = Projectile.alpha;
                dust2.noGravity = true;
                dust2.scale = 1f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 20; k++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.VilePowder, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
                dust.velocity *= 2f;
                dust.noGravity = true;
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PinkTorch, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
                dust2.noGravity = true;
                dust.velocity *= 1.5f;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Color.White with { A = 0 } * Projectile.Opacity);
            return false;
        }
    }
}