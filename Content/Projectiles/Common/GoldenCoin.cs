using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Dusts;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class GoldenCoin : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.GoldCoin}";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 0;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.extraUpdates = 1;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            float maxVel = Projectile.ai[1] == 1f ? 7f : 3.5f;
            if (Projectile.velocity.Length() > maxVel)
            {
                Projectile.velocity *= 0.94f;
            }

            Dust dust = Dust.NewDustPerfect(Projectile.position, ModContent.DustType<GoldCoin>());
            dust.noGravity = true;
            dust.alpha = Projectile.alpha;
            dust.position = Projectile.Center - Projectile.velocity;
            dust.velocity *= 0.3f;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Projectile.VampireKnivesAI(ai: 0, maxTime: 30f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Dust dust = Dust.NewDustPerfect(Projectile.position, ModContent.DustType<GoldCoin>());
            dust.noGravity = true;
            dust.velocity -= Projectile.velocity * 0.5f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
    }
}
