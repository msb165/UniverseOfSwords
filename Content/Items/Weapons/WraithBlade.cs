using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class WraithBlade : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 32;
            Item.height = 32;			
			Item.scale = 1.0F;
            Item.rare = ItemRarityID.LightPurple;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 16;
            Item.useAnimation = 16;           
            Item.damage = 54;
            Item.knockBack = 8.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 62800;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}