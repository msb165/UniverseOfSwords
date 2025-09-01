using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class PinkIceSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1.6F;
            Item.rare = 0;            
            Item.useStyle = 1;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 25; 
            Item.knockBack = 6.5F;
            Item.UseSound = SoundID.Item1;
            Item.value = 45000;			
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
            recipe.AddIngredient(ItemID.PinkIceBlock, 25);
            recipe.AddTile(TileID.WorkBenches);			
            recipe.Register();
	    }
    }
}