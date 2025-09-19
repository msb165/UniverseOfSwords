using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Materials
{
    public class Orichalcon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orihalcon");
            // Tooltip.SetDefault("Most powerful and rarest ore for making swords");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(gold: 1, silver: 80);
            Item.rare = ItemRarityID.LightPurple;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar)
                .AddIngredient(ItemID.SoulofLight)
                .AddIngredient(ItemID.SoulofNight)
                .AddIngredient(ItemID.PixieDust, 10)
                .AddIngredient(ItemID.FrostCore, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}