using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class DirtSword : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1.0F;
            Item.rare = 0;            
            Item.useStyle = 1;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 6; 
            Item.knockBack = 4.5F;
            Item.UseSound = SoundID.Item1;
            Item.value = 15;			
            Item.autoReuse = false; 
            Item.DamageType = DamageClass.Melee;
	    }
	           
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 25);
            recipe.AddTile(TileID.WorkBenches);			
            recipe.Register();
	    }
    }
}
