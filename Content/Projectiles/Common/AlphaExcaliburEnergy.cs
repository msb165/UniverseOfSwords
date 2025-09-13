using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Drawing;
using UniverseOfSwordsMod.Content.Projectiles.Common.Base;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class AlphaExcaliburEnergy : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Pink;
        public override Color BackColor => new(120, 50, 180);
        public override Color MiddleColor => Color.MediumPurple;
        public override Color FrontColor => Color.Pink;
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
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge, settings, Projectile.owner);

            if (UniverseUtils.IsAValidTarget(target))
            {
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.MediumPurple, Projectile.owner, damageDone, 255, 0.7f);
            }
        }
    }
}
