using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class ProbeLaser : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetDefaults()
        {
            Projectile.Size = new(8);
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 100;
        }

        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.TheDestroyer, Vector2.Zero);
                dust.noGravity = true;
                dust.position = Projectile.Center - Projectile.velocity / 3f * i;                
                dust.scale = 1f;
            }
        }
    }
}
