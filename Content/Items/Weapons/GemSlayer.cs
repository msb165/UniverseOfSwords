using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Placeable;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GemSlayer : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Inflicts Midas debuff on enemies");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1.5F;
            Item.rare = ItemRarityID.Orange;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 24;
            Item.useAnimation = 24;           
            Item.damage = 29; 
            Item.knockBack = 5.7F;
            Item.UseSound = SoundID.Item1;
            Item.value = 20000;			
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
            recipe.AddIngredient(null, "TopazSword", 1);  
            recipe.AddIngredient(null, "SapphireSword", 1);   
            recipe.AddIngredient(null, "EmeraldSword", 1);	
            recipe.AddIngredient(null, "AmethystSword", 1);		
            recipe.AddIngredient(null, "EmeraldSword", 1);	
            recipe.AddIngredient(null, "AmberSword", 1);	
            recipe.AddIngredient(null, "DiamondSword", 1);
			recipe.AddIngredient(null, "RubySword", 1);
            recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);			
            recipe.AddTile(TileID.Anvils);			
            recipe.Register();
	    }
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Midas, 360); // 6 second
        }
    }
}
