using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;
using static Terraria.ModLoader.ModContent;


namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class TrueGemSword : BaseCustomSwing
    {
        public override float SwordLength => 70f;
        public override bool DebugMode => false;
        public override string Texture => ModContent.GetInstance<Items.Weapons.TrueGemSword>().Texture;

        public override ref float SwingDirection => ref Projectile.ai[1];

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target))
            {
                target.AddBuff(BuffID.Midas, 360);
                if (!UniverseUtils.IsAValidTarget(target))
                {
                    return;
                }
                Vector2 newVel = (Main.MouseWorld - Player.Center).SafeNormalize(Vector2.Zero) * 8f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), Player.Center + newVel, newVel, ProjectileType<GemBolt>(), (int)(damageDone * 0.8), hit.Knockback, Player.whoAmI, ai0: Main.rand.Next(6));
                int projAmount = Main.rand.Next(3, 6);
                for (int i = 0; i < projAmount; i++)
                {
                    Vector2 velocity = Utils.RandomVector2(Main.rand, -100f, 101f);
                    while (velocity.Equals(Vector2.Zero))
                    {
                        velocity = Utils.RandomVector2(Main.rand, -100f, 101f);
                    }
                    velocity = velocity.SafeNormalize(-Vector2.UnitY) * Main.rand.Next(70, 101) * 0.1f;
                    if (Collision.SolidTiles(target.Center - velocity * 10f, 10, 10))
                    {
                        continue;
                    }
                    Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - velocity * 10f, velocity, ProjectileType<GemPulse>(), (int)(damageDone * 0.5), hit.Knockback * 0.8f, Player.whoAmI);
                }
            }
        }
    }
}
