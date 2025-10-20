using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class GemPulse : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetDefaults()
        {
            Projectile.Size = new(8);
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;
            Projectile.velocity.X *= 0.98f;

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemDiamond, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.25f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity *= 0.3f;
            }

            Dust dust3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemDiamond, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.25f);
            dust3.noGravity = true;
            dust3.velocity *= 0.1f;
        }
    }
}
