using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class ShadowFlame : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.alpha = 255;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.MaxUpdates = 3;
            Projectile.penetrate = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            Vector2 center = Projectile.Center;
            Projectile.scale = 1f - Projectile.localAI[0];
            Projectile.width = (int)(20f * Projectile.scale);
            Projectile.height = Projectile.width;
            Projectile.position = center - Projectile.Size / 2;
            if ((double)Projectile.localAI[0] < 0.1)
            {
                Projectile.localAI[0] += 0.01f;
            }
            else
            {
                Projectile.localAI[0] += 0.025f;
            }
            if (Projectile.localAI[0] >= 0.95f)
            {
                Projectile.Kill();
            }
            Projectile.velocity.X += Projectile.ai[0] * 1.5f;
            Projectile.velocity.Y += Projectile.ai[1] * 1.5f;
            if (Projectile.velocity.Length() > 16f)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= 16f;
            }
            Projectile.ai[0] *= 1.05f;
            Projectile.ai[1] *= 1.05f;
            if (Projectile.scale < 1f)
            {
                for (int i = 0; i < Projectile.scale * 10f; i++)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X, Projectile.velocity.Y, 255, default, 1.125f);
                    Main.dust[dust].position = (Main.dust[dust].position + Projectile.Center) / 2f;
                    Main.dust[dust].noGravity = true;
                    Dust dust2 = Main.dust[dust];
                    dust2.velocity *= 0.1f;
                    dust2 = Main.dust[dust];
                    dust2.velocity -= Projectile.velocity * (1.3f - Projectile.scale);
                    Main.dust[dust].fadeIn = 100 + Projectile.owner;
                    dust2 = Main.dust[dust];
                    dust2.scale += Projectile.scale * 0.75f;
                }
            }
        }
    }
}
