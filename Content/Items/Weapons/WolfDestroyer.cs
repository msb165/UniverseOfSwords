using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class WolfDestroyer : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Great for impersonating Werewolf hunters'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 32;
            Item.height = 32;			
			Item.scale = 1.9F;
            Item.rare = 6;            
            Item.useStyle = 1;             
            Item.useTime = 13;
            Item.useAnimation = 13;           
            Item.damage = 61;
            Item.knockBack = 8.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 71800;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}