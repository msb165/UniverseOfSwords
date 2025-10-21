using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class PoisonFang : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.PoisonFang}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 15;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(12);
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.timeLeft = 37;
            Projectile.noEnchantmentVisuals = true;
        }

        public ref float Timer => ref Projectile.ai[0];
        Player Player => Main.player[Projectile.owner];

        public override void AI()
        {
            if (Projectile.velocity.Length() > 3f)
            {
                Projectile.velocity *= 0.94f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if ((Projectile.alpha -= 50) < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.alpha == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PoisonStaff, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.velocity -= Projectile.velocity * 0.4f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 1800);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Poisoned, 1800);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PoisonStaff, 0f, 0f, 100, default, 1.2f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity *= 1.2f;
                dust2 = dust;
                dust2.velocity -= Projectile.oldVelocity * 0.3f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Color.White with { A = 0 } * Projectile.Opacity);
            return false;
        }
    }
}
