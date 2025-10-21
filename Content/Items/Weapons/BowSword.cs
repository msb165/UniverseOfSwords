using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BowSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Uses arrows as ammo");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.1f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 24;
            Item.knockBack = 5.0F;
            Item.UseSound = SoundID.Item5;
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(silver: 50);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.WoodenBow, 1)
            .AddRecipeGroup(RecipeGroupID.IronBar, 15)
            .AddIngredient(ModContent.ItemType<SwordMatter>(), 60)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}