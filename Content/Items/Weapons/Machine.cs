using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Machine : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Pew, pew! Boom, boom!");
		}
		
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62; 
			Item.scale = 1.0F;
            Item.rare = 7;            
            Item.useStyle = 1;             
            Item.useTime = 20;
            Item.useAnimation = 20;           
            Item.damage = 62; 
            Item.knockBack = 3.5F;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 10);
            Item.shoot = ProjectileID.VortexBeaterRocket;
            Item.shootSpeed = 10;			
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
	        recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
			recipe.AddIngredient(null, "Orichalcon", 1);
			recipe.AddIngredient(null, "UpgradeMatter", 1);
			recipe.AddIngredient(ItemID.LaserRifle, 1);
			recipe.AddIngredient(null, "PrimeSword", 1);
			recipe.AddIngredient(null, "DestroyerSword", 1);
			recipe.AddIngredient(null, "TwinsSword", 1);
			recipe.AddIngredient(null, "SwordMatter", 200);
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
	    }
    }
}
