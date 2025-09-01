using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Ichor : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Bullet}";
        public override void SetDefaults()
        {
            Projectile.Size = new(32);
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Vector2.One * 14f, 4, 4, DustID.Ichor, Alpha: 100);
                dust.noGravity = true;
                dust.position -= Projectile.velocity / 3f * i;
                dust.velocity *= 0.1f;
                dust.velocity += Projectile.velocity * 0.5f;
            }

            if (Main.rand.NextBool(8))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Vector2.One * 16f, 0, 0, DustID.Ichor, Alpha: 100, Scale: 0.5f);
                dust.velocity *= 0.25f;
                Dust dust2 = dust;
                dust2.velocity += Projectile.velocity * 0.5f;
            }

            Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y + 0.075f, -8f, 8f);
            if ((Projectile.scale -= 0.002f) <= 0f)
            {
                Projectile.Kill();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Resize(10, 10);
            return true;
        }
    }
}
