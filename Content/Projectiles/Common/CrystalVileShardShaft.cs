using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class CrystalVileShardShaft : BaseVilethorn
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.CrystalVileShardShaft}";

        public override int TipToSpawn => ModContent.ProjectileType<CrystalVileShardTip>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
            if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
            {
                for (int i = 0; i < 8; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Main.rand.Next(68, 71), Projectile.velocity.X * 0.025f, Projectile.velocity.Y * 0.025f, 200, default, 1.3f);
                    dust.noGravity = true;
                    Dust dust2 = dust;
                    dust2.velocity *= 0.5f;
                }
            }
        }
    }
}
