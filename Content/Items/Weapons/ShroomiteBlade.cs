using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class ShroomiteBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1.9F;
            Item.rare = 7;            
            Item.useStyle = 1;             
            Item.useTime = 15;
            Item.useAnimation = 15;           
            Item.damage = 70; 
            Item.knockBack = 7.2F;
            Item.UseSound = SoundID.Item1;
            Item.value = 280000;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
        
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShroomiteBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
	    }
    }
}