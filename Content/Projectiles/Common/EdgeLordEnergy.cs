using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class EdgeLordEnergy : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Red;
        public override Color BackColor => Color.DarkRed;
        public override Color MiddleColor => Color.PaleVioletRed;
        public override Color FrontColor => new(255, 64, 64);
        public override float ScaleAdd => 2f;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target) && Main.myPlayer == Projectile.owner)
            {
                target.AddBuff(BuffID.Ichor, 360);
                target.AddBuff(BuffID.Weak, 360);
                target.AddBuff(BuffID.Bleeding, 360);
                for (int i = 0; i < 10; i++)
                {
                    Vector2 spawnVel = (Vector2.UnitY * 16f).RotatedBy(i * MathHelper.TwoPi / 10f);
                    Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - spawnVel * 10f, spawnVel, ModContent.ProjectileType<EdgeSword>(), Projectile.damage, 4f, Projectile.owner, ai1: Main.rand.Next(14));
                }
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
            }
        }
    }
}
