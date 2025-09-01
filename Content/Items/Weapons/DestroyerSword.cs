using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class DestroyerSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Boom, Boom!");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 32;
            Item.height = 32; 
			Item.scale = 1.9F;
            Item.rare = 6;            
            Item.useStyle = 1;             
            Item.useTime = 20;   
            Item.useAnimation = 20; 			
            Item.damage = 62; 
            Item.knockBack = 6.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 160000;		
            Item.shoot = ProjectileID.RocketI;
            Item.shootSpeed = 10;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}
