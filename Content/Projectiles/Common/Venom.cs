using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class Venom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(18);
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.velocity.Length() > 3.25f)
            {
                Projectile.velocity *= 0.94f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.VampireKnivesAI(ai: 0, 30f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.TeleportationPotion, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.TeleportationPotion, Projectile.oldVelocity.X * 0.10f, Projectile.oldVelocity.Y * 0.10f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 500);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Projectile.GetAlpha(lightColor));
            return false;
        }
    }
}