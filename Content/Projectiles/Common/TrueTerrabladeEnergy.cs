using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class TrueTerrabladeEnergy : BaseEnergySwing
    {
        public override float BaseScale => 1f;

        public override float ScaleAdd => 1.2f;

        public override Color BackColor => new(45, 124, 205);
        public override Color MiddleColor => new(34, 177, 76);

        public override Color FrontColor => new(181, 230, 29);

        public override Color DustColorFrom => Color.Lime;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 positionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
            ParticleOrchestraSettings orchestraSettings = default;
            orchestraSettings.PositionInWorld = positionInWorld;
            ParticleOrchestraSettings settings = orchestraSettings;
            settings.MovementVector = Projectile.velocity;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TerraBlade, settings, Projectile.owner);

            base.OnHitNPC(target, hit, damageDone);
            if (UniverseUtils.IsAValidTarget(target) && Projectile.owner == Main.myPlayer)
            {
                Vector2 position = Player.RotatedRelativePoint(Player.MountedCenter);
                Vector2 velocity = (target.Center - position).SafeNormalize(Vector2.Zero) * Player.HeldItem.shootSpeed;
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), position + Vector2.Normalize(velocity).RotatedByRandom(MathHelper.PiOver2), velocity, ModContent.ProjectileType<TrueTerrablade>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
