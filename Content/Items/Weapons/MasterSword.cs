using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MasterSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Great for impersonating pot smashers'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.0F;
            Item.rare = 3;            
            Item.useStyle = 1;             
            Item.useTime = 23;
            Item.useAnimation = 25;           
            Item.damage = 25; 
            Item.knockBack = 4.1F;
            Item.UseSound = SoundID.Item1;
            Item.value = 9800;			
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
            recipe.AddIngredient(null, "SwordMatter", 80);
			recipe.AddRecipeGroup("IronBar", 20);
			recipe.AddIngredient(ItemID.CopperBroadsword, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordMatter", 80);
			recipe.AddRecipeGroup("IronBar", 20);
			recipe.AddIngredient(ItemID.TinBroadsword, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
	    } 
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Midas, 360); // 6 second
        }
    }
}
