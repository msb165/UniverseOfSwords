using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Consumables
{
    public class Skooma : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skooma");
            // Tooltip.SetDefault("Increases movement speed and jump height");
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;                
            Item.useStyle = ItemUseStyleID.DrinkLiquid;         
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = Item.CommonMaxStack;             
            Item.consumable = true;         
            Item.width = 22;
            Item.height = 40;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Purple;
            Item.buffType = ModContent.BuffType<Buffs.Skooma>();   
            Item.buffTime = 8000;    
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PurpleMucos);
            recipe.AddIngredient(ItemID.CandyCorn, 10);
            recipe.AddIngredient(ItemID.Moonglow);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
