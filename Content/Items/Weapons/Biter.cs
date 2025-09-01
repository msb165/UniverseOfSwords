using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Biter : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("It's sharp!");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 64;
            Item.height = 64;			
            Item.rare = ItemRarityID.Orange;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 17;
            Item.useAnimation = 17;           
            Item.damage = 26;
            Item.knockBack = 6.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 20800;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}