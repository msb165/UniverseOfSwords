using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PumpkinSword : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.125f;
            Item.rare = ItemRarityID.Blue;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 14; 
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 18);
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
			recipe.AddIngredient(ItemID.Pumpkin, 15);
            recipe.AddTile(TileID.WorkBenches);			
            recipe.Register();
	    }
    }
}
