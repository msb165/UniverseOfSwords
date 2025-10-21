using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class DeusExcaliburBeam : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.TexturesPath}Empty";

        public override void SetDefaults()
        {
            Projectile.Size = new(12);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.noEnchantmentVisuals = true;
            Projectile.tileCollide = false;
        }

        float velocityLength = 0f;

        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, Alpha: 100, newColor: Color.DeepSkyBlue, Scale: 1.5f);
                dust.scale = 1.5f;
                dust.position = Projectile.Center - Projectile.velocity / 5f * i;
                dust.noGravity = true;
                dust.velocity *= 0f;
            }

            if (Projectile.localAI[0] == 0f)
            {
                velocityLength = Projectile.velocity.Length();
                Projectile.localAI[0] = 1f;
            }

            UniverseUtils.Misc.FindNPCAndApplySpeed(Projectile, velocityLength, 500f, 4f);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Damage();
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, Alpha: 100, newColor: Color.DeepSkyBlue, Scale: 4f);
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
    }
}
