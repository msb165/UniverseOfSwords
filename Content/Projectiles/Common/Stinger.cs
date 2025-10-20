using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class Stinger : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.Stinger}";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new(8);
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.JungleSpore, Alpha: 100);
            dust.noGravity = true;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.JungleSpore, Alpha: 100);
                dust.noGravity = true;
                dust.velocity *= 2f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Projectile.GetAlpha(lightColor));
            return false;
        }
    }
}
