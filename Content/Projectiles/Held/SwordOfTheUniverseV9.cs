using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class SwordOfTheUniverseV9 : BaseCustomSwing
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.SwordOfTheUniverseV9>().Texture;

        public override float SwordLength => 200f;

        public override ref float SwingDirection => ref Projectile.ai[1];

        public override void AI()
        {
            base.AI();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
                target.AddBuff(ModContent.BuffType<SuperVenom>(), 360);
            }

            if (Main.myPlayer == Projectile.owner && UniverseUtils.IsAValidTarget(target))
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 newVel = (target.Center - Player.Center).SafeNormalize(Vector2.Zero) * 4f;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Player.Center + newVel.RotatedByRandom(MathHelper.ToRadians(90f)) * 3f, newVel.RotatedByRandom(MathHelper.ToRadians(5f)) * Main.rand.NextFloat(0.5f, 1f), Player.HeldItem.shoot, Player.HeldItem.damage * 3, Player.HeldItem.knockBack, Projectile.owner);
                }
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
