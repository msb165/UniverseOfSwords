using Microsoft.Xna.Framework;
using Terraria;
using UniverseOfSwords.Content.Projectiles.Base;

namespace UniverseOfSwords.Content.Projectiles.Common
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
