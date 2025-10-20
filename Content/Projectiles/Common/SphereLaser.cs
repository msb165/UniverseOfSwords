using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class SphereLaser : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 100;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 projPos = Projectile.position;
                projPos -= Projectile.velocity * (i * 0.25f);
                Projectile.alpha = 255;
                Dust dust = Dust.NewDustPerfect(projPos, DustID.MagnetSphere);
                dust.position = projPos + Projectile.Size / 2;
                dust.scale = Main.rand.Next(70, 110) * 0.013f;
                Dust dust2 = dust;
                dust2.velocity *= 0.2f;
            }
        }
    }
}
