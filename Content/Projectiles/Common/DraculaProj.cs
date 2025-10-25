using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class DraculaProj : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.VampireKnife}";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.light = 0.2f;
            Projectile.ignoreWater = true;
            Projectile.noEnchantmentVisuals = true;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }

        public int attackTarget = -1;
        float velocityLength = 0f;
        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                velocityLength = Projectile.velocity.Length();
                Projectile.localAI[0] = 1f;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.VampireKnivesAI(ai: 0, 30f);
            UniverseUtils.Misc.FindNPCAndApplySpeed(Projectile, velocityLength, velocity: 8f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Main.myPlayer == Projectile.owner)
            {
                UniverseUtils.Spawn.VampireHeal(damageDone, Main.player[Projectile.owner].Center, target, Main.player[Projectile.owner]);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.TheDestroyer, Alpha: 100, Scale: 0.8f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity *= 1.25f;
                dust2 = dust;
                dust2.velocity -= Projectile.oldVelocity * 0.3f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Color.White with { A = 40 } * Projectile.Opacity);
            return false;
        }
    }
}
