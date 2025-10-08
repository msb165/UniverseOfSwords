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
            Item.useStyle = ItemUseStyleID.DrinkLiquid;                 //this is how the item is holded when used
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 30;                 //this is where you set the max stack of item
            Item.consumable = true;           //this make that the item is consumable when used
            Item.width = 22;
            Item.height = 40;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Purple;
            Item.buffType = ModContent.BuffType<Buffs.Skooma>();    //this is where you put your Buff name
            Item.buffTime = 8000;    //this is the buff duration        20000 = 6 min
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PurpleMucos, 1);
            recipe.AddIngredient(ItemID.CandyCorn, 10);
            recipe.AddIngredient(ItemID.Moonglow, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
