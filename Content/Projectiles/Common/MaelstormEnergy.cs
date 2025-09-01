using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using UniverseOfSwordsMod.Content.Projectiles.Common.Base;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class MaelstormEnergy : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Cyan;
        public override Color BackColor => new(27, 153, 222);
        public override Color MiddleColor => new(80, 190, 250);
        public override Color FrontColor => new(140, 210, 255);
        public override float ScaleAdd => 1f;
        public override float BaseScale => 1.2f;

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Center, FrontColor.ToVector3());
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 positionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
            ParticleOrchestraSettings orchestraSettings = default;
            orchestraSettings.PositionInWorld = positionInWorld;
            ParticleOrchestraSettings settings = orchestraSettings;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);

            if (Player.altFunctionUse == 2)
            {
                return;
            }

            if (Main.myPlayer == Projectile.owner && UniverseUtils.IsAValidTarget(target))
            {
                Vector2 targetPos = target.Center;
                for (int i = 0; i < 13; i++)
                {
                    Vector2 spawnPos = Player.Center + new Vector2(Main.rand.Next(-200, 201), -600f);
                    spawnPos.Y -= 100 * i;
                    Vector2 spawnVel = targetPos - spawnPos;
                    if (spawnVel.Y < 0f)
                    {
                        spawnVel.Y *= -1f;
                    }
                    if (spawnVel.Y < 20f)
                    {
                        spawnVel.Y = 20f;
                    }
                    spawnVel = Vector2.Normalize(spawnVel) * 16f + (Vector2.One * Main.rand.Next(-40, 41) * 0.02f);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), spawnPos, spawnVel, Player.HeldItem.shoot, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f);
                }
            }
        }
    }
}
