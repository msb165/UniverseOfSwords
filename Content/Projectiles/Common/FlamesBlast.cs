using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class FlamesBlast : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetDefaults()
        {
            Projectile.Size = new(150);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            for (int i = 0; i < 25; i++)
            {
                Vector2 spawnVel = Vector2.Normalize(Utils.RandomVector2(Main.rand, -10f, 11f)) * Main.rand.Next(3, 9);
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork);
                dust.scale = 1.5f;
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-10f, 11f);
                dust.velocity = spawnVel;
            }
        }
    }
}
