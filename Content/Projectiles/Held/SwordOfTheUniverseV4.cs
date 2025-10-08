using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class SwordOfTheUniverseV4 : BaseCustomSwing
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.SwordOfTheUniverseV4>().Texture;

        public override float SwordLength => 160f;

        public override ref float SwingDirection => ref Projectile.ai[1];

        public override float BaseScale => 0.7f;

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

            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 position = Player.Center - Vector2.UnitY * 64f;
                Vector2 velocity = Vector2.Normalize(target.Center - position) * Player.HeldItem.shootSpeed;
                Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), position, velocity, Player.HeldItem.shoot, Player.HeldItem.damage * 2, Player.HeldItem.knockBack, Projectile.owner, 0f);
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
