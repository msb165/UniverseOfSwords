using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class PinkCrystalShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new(6);
            Projectile.aiStyle = ProjAIStyleID.CrystalShard;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.light = 0.5f;
            Projectile.alpha = 50;
            Projectile.scale = 1.2f;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White with { A = 0 } * Projectile.Opacity;
    }
}
