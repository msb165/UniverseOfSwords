using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Materials
{
	public class SwordShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 25;
			// Tooltip.SetDefault("Shard of lost sword");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = 0;
			Item.rare = ItemRarityID.LightRed;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 400);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
	    }
    }
}