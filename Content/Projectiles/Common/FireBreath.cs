using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class FireBreath : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 60;
            Projectile.noEnchantmentVisuals = true;
        }

        public ref float Timer => ref Projectile.ai[0];
        public override void AI()
        {
            Timer++;
            if (Timer > 7f)
            {
                float scale = 1f;
                if (Timer >= 8f && Timer <= 10f)
                {
                    // Makes the first bit zero so the timer can only have even numbers
                    // 2 / 8 = 0.25 and so on
                    scale = ((int)Timer & 0b_1110) / 8f;
                }
                if (Main.rand.NextBool(2))
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                    dust.noGravity = true;
                    Dust dust2;
                    if (Main.rand.NextBool(3))
                    {
                        dust2 = dust;
                        dust2.scale *= 3f;
                        dust.velocity *= 2f;
                    }
                    dust2 = dust;
                    dust2.scale *= 1.5f;
                    dust.velocity *= 1.2f;
                    dust2 = dust;
                    dust2.scale *= scale;
                    dust2.velocity += Projectile.velocity;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
    }
}
