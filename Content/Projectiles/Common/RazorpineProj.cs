using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Dusts;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class RazorpineProj : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.PineNeedleFriendly}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 0;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.GreenTorch);
            dust.noGravity = true;
            dust.alpha = Projectile.alpha;
            dust.position = Projectile.Center - Projectile.velocity;
            dust.velocity *= 0.3f;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Projectile.SimpleFadeOut(ai: 0, 25f);
            if (Projectile.ai[0] > 12f)
            {
                Projectile.ai[0] = 12f;
                Projectile.velocity.Y += 0.5f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Damage();
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.GreenTorch);
                dust.noGravity = true;
                dust.velocity -= Projectile.velocity * 0.5f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Projectile.GetAlpha(lightColor));
            return false;
        }
    }
}
