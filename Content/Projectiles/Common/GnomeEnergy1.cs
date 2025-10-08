using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class GnomeEnergy1 : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Red;
        public override Color BackColor => Color.DarkRed;
        public override Color MiddleColor => Color.Red;
        public override Color FrontColor => new Color(255, 127, 127);
        public override float ScaleAdd => 1f;
        public override float BaseScale => 1.2f;

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Center, FrontColor.ToVector3());
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.Ichor, 360);
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.OnFire, 360);
            target.AddBuff(BuffID.Poisoned, 360);
            target.AddBuff(BuffID.Venom, 360);
            target.AddBuff(BuffID.Confused, 360);
            target.AddBuff(BuffID.CursedInferno, 360);
            target.AddBuff(BuffID.Slimed, 360);
            if (Main.myPlayer == Projectile.owner && UniverseUtils.IsAValidTarget(target))
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ModContent.ProjectileType<SuperExplosion>(), damageDone, hit.Knockback, Projectile.owner);

                int direction = Utils.SelectRandom(Main.rand, 1, -1);
                float spawnX = Main.screenPosition.X;
                if (direction < 0)
                {
                    spawnX += Main.screenWidth;
                }
                float spawnY = Main.screenPosition.Y;
                spawnY += Main.rand.Next(Main.screenHeight);
                Vector2 spawnPos = new(spawnX, spawnY);
                Vector2 spawnVel = target.Center - spawnPos;
                spawnVel.X += Main.rand.Next(-50, 51) * 0.1f;
                spawnVel.Y += Main.rand.Next(-50, 51) * 0.1f;
                spawnVel = Vector2.Normalize(spawnVel) * 24f;
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), spawnPos, spawnVel, ModContent.ProjectileType<GnomSword>(), damageDone, 0f, Projectile.owner, ai1:Main.rand.Next(14));

                Vector2 targetPos = target.Center;
                Vector2 spawnPos2 = Player.Center + new Vector2(-Main.rand.Next(0, 401), -600f);
                spawnPos2.Y -= 100;
                Vector2 spawnVel2 = targetPos - spawnPos2;
                if (spawnVel2.Y < 0f)
                {
                    spawnVel2.Y *= -1f;
                }
                if (spawnVel2.Y < 20f)
                {
                    spawnVel2.Y = 20f;
                }
                spawnVel2 = Vector2.Normalize(spawnVel2) * 16f;
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), spawnPos2, spawnVel2, ModContent.ProjectileType<GnomSword>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f);
            }
        }
    }
}
