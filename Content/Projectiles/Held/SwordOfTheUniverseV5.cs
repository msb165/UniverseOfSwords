using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class SwordOfTheUniverseV5 : BaseCustomSwing
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.SwordOfTheUniverseV5>().Texture;

        public override float SwordLength => 200f;

        public override ref float SwingDirection => ref Projectile.ai[1];

        public ref float Timer2 => ref Projectile.ai[2];

        public override float BaseScale => 0.6f;

        public override void AI()
        {
            base.AI();
            if (Main.myPlayer == Projectile.owner && Player.itemAnimation % 3 == 0)
            {
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - Player.Center) * Player.HeldItem.shootSpeed;
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Player.Center, velocity, Player.HeldItem.shoot, Player.HeldItem.damage, Player.HeldItem.knockBack, Projectile.owner, 0f, (Main.rand.NextFloat() - 0.5f) * MathHelper.PiOver4);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
                target.AddBuff(ModContent.BuffType<SuperVenom>(), 360);
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
