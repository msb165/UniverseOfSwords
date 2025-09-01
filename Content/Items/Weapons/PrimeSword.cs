using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class PrimeSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Pew, pew!");
		}
		
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32; 
			Item.scale = 1.9F;
            Item.rare = 6;            
            Item.useStyle = 1;             
            Item.useTime = 15;   
            Item.useAnimation = 15; 			
            Item.damage = 65; 
            Item.knockBack = 6.0F;
            Item.UseSound = SoundID.Item33;
            Item.value = 160000;		
			Item.shoot = ProjectileID.LaserMachinegunLaser ;
            Item.shootSpeed = 20;
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}
