using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SnowSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Let it snow, let it snow...'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1.4F;
            Item.rare = 1;            
            Item.useStyle = 1;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 10; 
            Item.knockBack = 1.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 100;
            Item.autoReuse = false; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
        
		public override void AddRecipes()
	    {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SnowBlock, 25);
            recipe.AddTile(TileID.WorkBenches);			
            recipe.Register();
	    }
    }
}
