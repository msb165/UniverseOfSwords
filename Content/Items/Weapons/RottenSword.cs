using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class RottenSword : ModItem
    {
        public override void SetDefaults()
        {; 
            Item.width = 35;
            Item.height = 35; 
			Item.scale = 2.0F;
            Item.rare = ItemRarityID.LightPurple;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;
            Item.useAnimation = 20;           
            Item.damage = 62; 
            Item.knockBack = 5.0F;
             Item.UseSound = SoundID.Item8;
			Item.shoot = ProjectileID.NettleBurstRight;
            Item.shootSpeed = 20;
            Item.value = 304200;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}