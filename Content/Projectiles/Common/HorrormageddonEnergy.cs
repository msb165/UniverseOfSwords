using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class HorrormageddonEnergy : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Green;
        public override Color BackColor => Color.DarkGreen;
        public override Color MiddleColor => Color.Green;
        public override Color FrontColor => Color.Lime;
        public override float ScaleAdd => 2f;
        public override float BaseScale => 1.4f;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (UniverseUtils.IsAValidTarget(target) && Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ModContent.ProjectileType<HorrorBlast>(), Projectile.damage, 4f, Projectile.owner);
            }
        }
    }
}
