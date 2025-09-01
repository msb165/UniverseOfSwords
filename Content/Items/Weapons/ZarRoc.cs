using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class ZarRoc : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1.7F;
            Item.rare = 5;            
            Item.useStyle = 1;             
            Item.useTime = 10;
            Item.useAnimation = 10;           
            Item.damage = 68; 
            Item.knockBack = 10.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 590000;			
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
            recipe.AddIngredient(ItemID.Excalibur, 1);
			recipe.AddIngredient(ItemID.Ruby, 1);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(TileID.DemonAltar);			
            recipe.Register();
	    }
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
           target.AddBuff(BuffID.Confused, 360); // 6 second
        }
    }
}
