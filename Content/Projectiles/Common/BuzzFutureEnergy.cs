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
    public class BuzzFutureEnergy : BaseEnergySwing
    {
        public override Color BackColor => Color.DarkCyan;
        public override Color MiddleColor => Color.Cyan;
        public override Color FrontColor => Color.LightCyan;
        public override Color DustColorFrom => Color.Cyan;
        public override Color DustColorTo => Color.LightCyan;

        public override float ScaleAdd => 0.6f;

        public override float BaseScale => 1.2f;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 spawnPos = Player.Center + new Vector2(Main.rand.Next(-300, 301), -600f);
                    spawnPos.Y -= 100 * i;
                    Vector2 spawnVel = target.Center - spawnPos;
                    if (spawnVel.Y < 0f)
                    {
                        spawnVel.Y *= -1f;
                    }
                    if (spawnVel.Y < 20f)
                    {
                        spawnVel.Y = 20f;
                    }
                    spawnVel = Vector2.Normalize(spawnVel) * 8f;
                    Projectile.NewProjectileDirect(target.GetSource_OnHit(target), spawnPos, spawnVel, ModContent.ProjectileType<CyanBolt>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
        }
    }
}
