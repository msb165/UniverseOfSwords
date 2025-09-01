using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GoblinKnife : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Fast and small knife of Goblins");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 40;
            Item.height = 40; 
            Item.rare = 2;            
            Item.useStyle = 1;             
            Item.useTime = 9;
            Item.useAnimation = 9;           
            Item.damage = 15; 
            Item.knockBack = 2.5F;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 10);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}