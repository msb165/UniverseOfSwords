using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class SwordOfTheUniverseV8 : BaseCustomSwing
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.SwordOfTheUniverseV8>().Texture;

        public override float SwordLength => 180f;

        public override ref float SwingDirection => ref Projectile.ai[1];

        public override void AI()
        {
            base.AI();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<SuperVenom>(), 360);
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }

            float piOverTen = MathHelper.Pi / 10f;
            Vector2 baseVel = (target.Center - Player.Center).SafeNormalize(Vector2.Zero) * 40f;
            Vector2 newVel = (target.Center - Player.Center).SafeNormalize(Vector2.Zero) * 15f;
            for (int i = 0; i < 5; i++)
            {
                float offset = i - (5f - 1f) / 2f;
                Vector2 velOffset = baseVel.RotatedBy(piOverTen * offset);
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), Player.RotatedRelativePoint(Player.MountedCenter) + velOffset, newVel, ModContent.ProjectileType<SOTU8>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            float rotation = Projectile.rotation - MathHelper.PiOver4;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + rotation.ToRotationVector2() * SwordLength * Projectile.scale, 16f, ref _);
        }
    }
}
