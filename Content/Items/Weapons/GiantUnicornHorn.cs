using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GiantUnicornHorn : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Fabolous!");
		}
		
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 1.7F;
            Item.rare = 6;            
            Item.useStyle = 1;             
            Item.useTime = 15;
            Item.useAnimation = 15;           
            Item.damage = 61; 
            Item.knockBack = 7.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 153000;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}