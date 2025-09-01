using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class ElBastardo : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'The legendary El Bastardo'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 44;
            Item.height = 44;			
			Item.scale = 1.3F;
            Item.rare = 6;           
            Item.useStyle = 1;             
            Item.useTime = 16;
            Item.useAnimation = 16;           
            Item.damage = 50;
            Item.knockBack = 7.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 5);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
    }
}