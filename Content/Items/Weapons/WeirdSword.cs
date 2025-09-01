using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class WeirdSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("This sword is weird..");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 32;
            Item.height = 32;			
			Item.scale = 2.3F;
            Item.rare = 2;            
            Item.useStyle = 1;             
            Item.useTime = 13;
            Item.useAnimation = 13;           
            Item.UseSound = SoundID.Item1;
            Item.value = 500;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}