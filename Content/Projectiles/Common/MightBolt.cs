using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class MightBolt : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetDefaults()
        {
            Projectile.Size = new(10);
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Vector2 spawnSpeed = Vector2.Normalize(Projectile.velocity) * 10f;
            short dustType = DustID.Clentaminator_Blue;

            if (Projectile.ai[0] == 0f)
            {
                int direction = Utils.SelectRandom(Main.rand, -1, 1);
                for (int i = 0; i < 8; i++)
                {
                    Vector2 spinPoint = (Vector2.UnitY).RotatedBy(i * MathHelper.TwoPi / 8f);
                    Dust dust = Dust.NewDustPerfect(Projectile.position, dustType);
                    dust.scale = 1.5f;
                    dust.noGravity = true;
                    dust.position = Projectile.Center + (spinPoint * 16f * direction).RotatedBy(Projectile.velocity.ToRotation());
                    dust.velocity = Vector2.Normalize(spinPoint);
                }

                Projectile.ai[0] = 1f;
            }

            Lighting.AddLight(Projectile.position, Color.Blue.ToVector3());
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.position, dustType);
                dust.scale = 1.5f;
                dust.noGravity = true;
                dust.position = Projectile.Center - Projectile.velocity / 3f * i;
                dust.velocity = -spawnSpeed.RotatedBy(-MathHelper.PiOver2) / 10f;

                Dust dust2 = Dust.NewDustPerfect(Projectile.position, dustType);
                dust2.scale = 1.5f;
                dust2.noGravity = true;
                dust2.position = Projectile.Center - Projectile.velocity / 3f * i;
                dust2.velocity = spawnSpeed.RotatedBy(-MathHelper.PiOver2) / 10f;

                Dust dust3 = Dust.NewDustPerfect(Projectile.position, dustType);
                dust3.scale = 1f;
                dust3.noGravity = true;
                dust3.position = Projectile.Center - Projectile.velocity / 3f * i;
                dust3.velocity *= 0.5f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Damage();
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, 0, 0, DustID.Clentaminator_Blue);
                dust.scale = 1.5f;
                dust.noGravity = true;
                dust.velocity *= 2f;
            }
        }
    }
}
