using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Drawing;
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
            if (UniverseUtils.IsAValidTarget(target))
            {
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.Red, Projectile.owner, damageDone, 200, 0.5f);
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.Red, Projectile.owner, damageDone, 200, 0.5f);
            }
        }
    }
}
