using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GelBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Slows down enemies");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 0.9F;
            Item.rare = 0;            
            Item.useStyle = 1;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 9; 
            Item.knockBack = 4.5F;
            Item.UseSound = SoundID.Item1;
            Item.value = 1500;			
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
            recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddTile(TileID.WorkBenches);			
            recipe.Register();
	    }
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Slimed, 360);		   
        }
    }
}
