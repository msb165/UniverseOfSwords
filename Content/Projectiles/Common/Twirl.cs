using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Twirl : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Typhoon}";

        public override void SetDefaults()
        {
            Projectile.Size = new(32);
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.timeLeft = 200;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.ai[0];
            Projectile.velocity *= Projectile.ai[1];
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Clentaminator_Blue, Vector2.Zero, Alpha: 100);
                dust.scale = 1.5f;
                dust.position = Projectile.Center - Projectile.velocity / 5f * i;
                dust.velocity = (Vector2.UnitY * 2f).RotatedBy(i * MathHelper.TwoPi / 2.5f).RotatedBy(Projectile.rotation) * Projectile.direction;
                dust.noGravity = true;

                Dust dust2 = Dust.NewDustPerfect(Projectile.Center, DustID.SpectreStaff, Vector2.Zero, Alpha: 250, Color.Blue with { A = 0 }, Scale: 1.5f);
                dust2.scale = 1.5f;
                dust2.position = Projectile.Center - Projectile.velocity / 5f * i;
                dust2.velocity = (Vector2.UnitY * 2f).RotatedBy(i * MathHelper.TwoPi / 2.5f).RotatedBy(Projectile.rotation) * Projectile.direction;
                dust2.noGravity = true;
            }
        }
    }
}
