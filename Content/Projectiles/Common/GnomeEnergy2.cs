using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common.Base;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class GnomeEnergy2 : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Blue;
        public override Color BackColor => new(27, 153, 222);
        public override Color MiddleColor => new(80, 190, 250);
        public override Color FrontColor => new(140, 210, 255);
        public override float ScaleAdd => 1f;
        public override float BaseScale => 1f;

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Center, FrontColor.ToVector3());
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
    }
}
