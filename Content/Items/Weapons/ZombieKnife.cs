using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class ZombieKnife : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Great for impersonating Zombie hunters'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;			
			Item.scale = 1.6F;
            Item.rare = ItemRarityID.Orange;           
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 15;
            Item.useAnimation = 15;           
            Item.damage = 7;
            Item.knockBack = 4.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 7800;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}