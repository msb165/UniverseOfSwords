using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DevilBlade : ModItem
    {
        public int swordCounter = 0;
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.damage = 74;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ProjectileID.UnholyTridentFriendly;
            Item.shootSpeed = 1f;
            Item.value = 351200;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swordCounter++;
            if (swordCounter > 2)
            {
                swordCounter = 0;
                Projectile.NewProjectile(source, position + velocity * 4f, velocity.RotatedByRandom(MathHelper.ToRadians(5f)) * 6f, ModContent.ProjectileType<UnholyTrident>(), damage, knockback, player.whoAmI);
                return false;
            }
            float offsetX = Main.rand.Next(10, 80) * 0.001f;
            if (Main.rand.NextBool(2))
            {
                offsetX *= -1f;
            }
            float offsetY = Main.rand.Next(10, 80) * 0.001f;
            if (Main.rand.NextBool(2))
            {
                offsetY *= -1f;
            }
            Projectile.NewProjectile(source, position + velocity * 12f, velocity * 4f, ModContent.ProjectileType<ShadowFlame>(), damage, knockback, player.whoAmI, offsetX, offsetY);
            return false;
        }
    }
}