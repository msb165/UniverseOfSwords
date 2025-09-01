using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BarbarianSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'We gonna need more Barbarians'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 58;
            Item.height = 58; 
			Item.scale = 1.0F;
            Item.rare = 1;            
            Item.useStyle = 1;             
            Item.useTime = 24;
            Item.useAnimation = 24;           
            Item.damage = 20; 
            Item.knockBack = 3.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 15000;			
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
			recipe.AddIngredient(ItemID.IronBroadsword, 1);
            recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddTile(TileID.Anvils);			
            recipe.Register();
			
			recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LeadBroadsword, 1);
			recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
	    }
    }
}