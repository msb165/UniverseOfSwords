using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class VenomSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 60;
            Item.useAnimation = 30;
            Item.knockBack = 4f;
            Item.damage = 63;
            Item.shoot = ModContent.ProjectileType<VenomFang>();
            Item.shootSpeed = 3f;
            Item.UseSound = SoundID.Item43;
            Item.value = Item.sellPrice(gold: 2);
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -60f), Vector2.UnitY * 6f);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(4); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(numberProjectiles * 2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                float offset = i - (numberProjectiles - 1f) / 2f;
                Projectile.NewProjectileDirect(source, position + velocity, velocity.RotatedBy(rotation * offset), type, damage / 5, knockback, player.whoAmI, ai1: 1f);
            }
            return false;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation.Y -= 1f * player.gravDir;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PoisonSword>())
                .AddIngredient(ItemID.PoisonStaff)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}