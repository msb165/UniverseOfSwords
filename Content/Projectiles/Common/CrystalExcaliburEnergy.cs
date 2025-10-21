using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    internal class CrystalExcaliburEnergy : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Red;
        public override Color BackColor => new(150, 0, 40, 250);
        public override Color MiddleColor => Color.Red with { A = 250 };
        public override Color FrontColor => new(255, 127, 127, 250);
        public override float ScaleAdd => 1.1f;
        public override float BaseScale => 1.4f;

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Center, FrontColor.ToVector3());
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Oiled, 300);
            if (UniverseUtils.IsAValidTarget(target))
            {
                for (int i = 0; i < 3; i++)
                {
                    UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.Red, Projectile.owner, Projectile.damage, 200, 0.5f);
                }
            }
        }
    }
}
