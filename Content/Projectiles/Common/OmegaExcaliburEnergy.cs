using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using UniverseOfSwordsMod.Content.Projectiles.Common.Base;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class OmegaExcaliburEnergy : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Cyan;
        public override Color BackColor => Color.DarkCyan;
        public override Color MiddleColor => Color.Cyan;
        public override Color FrontColor => Color.LightCyan;
        public override float ScaleAdd => 1f;
        public override float BaseScale => 1f;

        public override void AI()
        {
            base.AI();
            Lighting.AddLight(Projectile.Center, FrontColor.ToVector3());
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);

            Vector2 positionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
            ParticleOrchestraSettings orchestraSettings = default;
            orchestraSettings.PositionInWorld = positionInWorld;
            ParticleOrchestraSettings settings = orchestraSettings;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);

            if (UniverseUtils.IsAValidTarget(target))
            {
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.Cyan, Projectile.owner, damageDone);
            }
        }
    }
}
