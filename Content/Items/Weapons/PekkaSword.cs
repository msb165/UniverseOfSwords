using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class PekkaSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'DESTROY'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 62; 
			Item.scale = 1.0F;
            Item.rare = ItemRarityID.Blue;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 22;
            Item.useAnimation = 22;           
            Item.damage = 25; 
            Item.knockBack = 4.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 10000;			
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
			recipe.AddIngredient(ItemID.GoldBroadsword, 1);
            recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddTile(TileID.Anvils);			
            recipe.Register();
			
			recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
			recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
	    }
    }
}