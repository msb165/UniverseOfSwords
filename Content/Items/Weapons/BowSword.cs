using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BowSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Uses arrows as ammo");
		}
		
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;			
			Item.scale = 1.1F;
            Item.rare = ItemRarityID.Orange;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 25;
            Item.useAnimation = 25;           
            Item.damage = 24;
            Item.knockBack = 5.0F;
            Item.UseSound = SoundID.Item5;
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(silver: 50);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = AmmoID.Arrow;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
        
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.WoodenBow, 1);
			recipe.AddRecipeGroup("IronBar", 15);
			recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 60);
            recipe.AddTile(TileID.Anvils);			
            recipe.Register();
	    } 
    }
}