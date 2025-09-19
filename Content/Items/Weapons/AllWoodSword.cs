using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class AllWoodSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Smells like world'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 32;
            Item.height = 32; 
			Item.scale = 1f;
            Item.rare = ItemRarityID.Orange;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 12; 
            Item.knockBack = 1f;
            Item.UseSound = SoundID.Item1;
            Item.value = 6888;
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }	
                
		public override void AddRecipes()
	    {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wood, 15);
			recipe.AddIngredient(ItemID.BorealWood, 15);
			recipe.AddIngredient(ItemID.PalmWood, 15);
			recipe.AddIngredient(ItemID.RichMahogany, 15);
			recipe.AddIngredient(ItemID.Ebonwood, 15);
            recipe.AddTile(TileID.WorkBenches);			
            recipe.Register();
			
		    recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 15);
			recipe.AddIngredient(ItemID.BorealWood, 15);
			recipe.AddIngredient(ItemID.PalmWood, 15);
			recipe.AddIngredient(ItemID.RichMahogany, 15);
			recipe.AddIngredient(ItemID.Shadewood, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
	    }
    }
}