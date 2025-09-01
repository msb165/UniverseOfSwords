using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class ForceBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Sword with a very strong knockback'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;			
			Item.scale = 1.0F;
            Item.rare = 5;            
            Item.useStyle = 1;             
            Item.useTime = 40;
            Item.useAnimation = 40;           
            Item.damage = 50;
            Item.knockBack = 12.0F;
            Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(gold: 2);
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
			recipe.AddIngredient(ItemID.BreakerBlade, 1);
			recipe.AddIngredient(ItemID.CobaltBar, 11);
            recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddTile(TileID.Anvils);			
            recipe.Register();
			
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BreakerBlade, 1);
			recipe.AddIngredient(ItemID.PalladiumBar, 11);
            recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddTile(TileID.Anvils);			
            recipe.Register();
	    }
    }
}