using Microsoft.Build.Construction;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Net;
using UniverseOfSwordsMod.Content.Projectiles.Common.Base;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class StarDestroyerEnergy : BaseEnergySwing
    {
        public override Color BackColor => Color.DarkCyan;
        public override Color MiddleColor => Color.Cyan;
        public override Color FrontColor => Color.LightCyan;
        public override Color DustColorFrom => Color.Cyan;
        public override Color DustColorTo => Color.LightCyan;

        public override float ScaleAdd => base.ScaleAdd;

        public override float BaseScale => base.BaseScale;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 spawnPos = Player.Center + new Vector2(Main.rand.Next(-200, 201), -600f);
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
                    spawnVel = Vector2.Normalize(spawnVel) * 16f + (Vector2.One * Main.rand.Next(-40, 41) * 0.02f);
                    Projectile proj = Projectile.NewProjectileDirect(target.GetSource_OnHit(target), spawnPos, spawnVel, ProjectileID.LunarFlare, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    proj.DamageType = DamageClass.Melee;
                }
            }
        }
    }
}
