using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class InfluxWaverEnergy : BaseEnergySwing
    {
        public override Color DustColorFrom => Color.Cyan;
        public override Color BackColor => new(34, 108, 203);
        public override Color MiddleColor => new(113, 251, 255);
        public override Color FrontColor => new(210, 255, 255);
        public override float ScaleAdd => 0.8f;
        public override float BaseScale => 1f;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(BuffID.Electrified, 360);
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target))
            {
                Vector2 positionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
                ParticleOrchestraSettings orchestraSettings = default;
                orchestraSettings.PositionInWorld = positionInWorld;
                ParticleOrchestraSettings settings = orchestraSettings;
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);

                Vector2 v = Main.rand.NextVector2CircularEdge(200f, 200f);
                Vector2 vector = v.SafeNormalize(Vector2.UnitY) * 21f;
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center - vector * 20f, vector, ProjectileID.InfluxWaver, Projectile.damage, 0f, Projectile.owner);
            }
        }
    }
}
