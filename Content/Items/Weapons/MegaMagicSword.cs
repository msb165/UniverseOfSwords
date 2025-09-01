using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;


namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MegaMagicSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.3F;
            Item.rare = 7;            
            Item.useStyle = 1;             
            Item.useTime = 12;
            Item.useAnimation = 12;           
            Item.damage = 82; 
            Item.knockBack = 10.70F;
            Item.UseSound = new SoundStyle("Sounds/Item/Spell");
			Item.shoot = ProjectileID.SwordBeam;
            Item.shootSpeed = 20;
            Item.value = 410000;			
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
			recipe.AddIngredient(null, "MagicSword", 1);
			recipe.AddIngredient(null, "Orichalcon", 1);
			recipe.AddIngredient(null, "SwordMatter", 100);          
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
	    }
    }
}