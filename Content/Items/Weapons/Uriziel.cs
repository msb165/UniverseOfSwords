using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Uriziel : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Sword of great warrior called Uriziel");
		}
		
        public override void SetDefaults()
        {
            Item.width = 110;
            Item.height = 110;			
			Item.scale = 1.0F;
            Item.rare = 7;            
            Item.useStyle = 1;             
            Item.useTime = 13;
            Item.useAnimation = 13;           
            Item.damage = 130;
            Item.knockBack = 15.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 30);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
		
	    public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "SwordMatter", 200);
		    recipe.AddIngredient(null, "UpgradeMatter", 3);
			recipe.AddIngredient(null, "SwordShard", 1);
			recipe.AddIngredient(null, "WeirdSword", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Ichor, 360); 
           target.AddBuff(BuffID.OnFire, 360);			   
        }
    }
}