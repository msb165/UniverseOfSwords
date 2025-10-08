using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Machine : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Pew, pew! Boom, boom!");
		}
		
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62; 
			Item.scale = 1.0F;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;
            Item.useAnimation = 20;           
            Item.damage = 62; 
            Item.knockBack = 3.5F;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 10);
            Item.shoot = ProjectileID.VortexBeaterRocket;
            Item.shootSpeed = 10;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
        
		public override void AddRecipes()
        {
	    }
    }
}
