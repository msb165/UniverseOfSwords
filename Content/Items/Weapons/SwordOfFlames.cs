using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SwordOfFlames : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 78;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item73;
            Item.value = Item.sellPrice(silver: 65);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                Dust dust = Dust.NewDustDirect(player.Center + new Vector2(player.direction - 60f * -player.direction, player.gravDir * -80f).RotatedBy(player.itemRotation), 18, 18, DustID.InfernoFork, 0, 0, 127, default, 1.25f);
                if (player.direction == -1)
                {
                    dust.position.X -= 24f;
                }
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(2f, 4f) - Vector2.UnitY;
                dust.velocity = dust.velocity.RotatedBy(-player.itemRotation);
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.InfernoFork, dustScale: 1.25f, start: (int)(14 * Item.scale), end: (int)(84 * Item.scale));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 10f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<FlamesBolt>(), hit.Damage, hit.Knockback, player.whoAmI);
        }
    }
}