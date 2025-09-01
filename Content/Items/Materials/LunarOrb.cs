using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Materials
{
	public class LunarOrb : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Essence of Lunar Towers");
		}
		
		public override void SetDefaults()
		{
			Item.Size = new(40);
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Cyan;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FragmentSolar, 10);
			recipe.AddIngredient(ItemID.FragmentVortex, 10);
			recipe.AddIngredient(ItemID.FragmentNebula, 10);
			recipe.AddIngredient(ItemID.FragmentStardust, 10);
			recipe.AddIngredient(ItemID.SoulofLight, 15);
			recipe.AddIngredient(ItemID.SoulofNight, 15);
			recipe.AddIngredient(ItemID.SoulofFlight, 15);
			recipe.AddIngredient(ItemID.SoulofMight, 20);
			recipe.AddIngredient(ItemID.SoulofFright, 20);
			recipe.AddIngredient(ItemID.SoulofSight, 20);
			recipe.AddIngredient(null, "MartianSaucerCore" , 1);
			recipe.AddIngredient(ItemID.CelestialSigil, 1);
            recipe.AddTile(TileID.LunarCraftingStation);			
            recipe.Register();
	    }
	}
}