using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class SwordOfTheUniverseV6 : BaseCustomSwing
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.SwordOfTheUniverseV6>().Texture;

        public override float SwordLength => 180f;

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
                for (int i = 0; i < 4; i++)
                {
                    Vector2 velocity = Vector2.Normalize(target.Center - Player.Center) * 8f;
                    Vector2 spawnPos = Player.Center + velocity.RotatedByRandom(MathHelper.ToRadians(90f));
                    velocity = Vector2.Normalize(target.Center - spawnPos) * 8f;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), spawnPos, velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(1f, 1.5f), Player.HeldItem.shoot, Player.HeldItem.damage * 4, Player.HeldItem.knockBack / 2, Projectile.owner);
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
