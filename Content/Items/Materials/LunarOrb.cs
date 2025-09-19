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
            Item.ResearchUnlockCount = 5;
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
            CreateRecipe()
				.AddIngredient(ItemID.FragmentSolar, 10)
                .AddIngredient(ItemID.FragmentVortex, 10)
                .AddIngredient(ItemID.FragmentNebula, 10)
                .AddIngredient(ItemID.FragmentStardust, 10)
                .AddIngredient(ItemID.SoulofLight, 15)
                .AddIngredient(ItemID.SoulofNight, 15)
                .AddIngredient(ItemID.SoulofFlight, 15)
                .AddIngredient(ItemID.SoulofMight, 20)
                .AddIngredient(ItemID.SoulofFright, 20)
                .AddIngredient(ItemID.SoulofSight, 20)
                .AddIngredient(null, "MartianSaucerCore")
                .AddIngredient(ItemID.CelestialSigil)
                .AddTile(TileID.LunarCraftingStation)
				.Register();
	    }
	}
}