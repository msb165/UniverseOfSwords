using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class CrystalVileSword : ModItem
    {
		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Crystal Vile Sword");
		}

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 46; 
			Item.scale = 1f;
            Item.rare = ItemRarityID.LightPurple;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 50;
            Item.useAnimation = 50;           
            Item.damage = 64; 
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item101;
			Item.shoot = ProjectileID.CrystalVileShardShaft;
            Item.shootSpeed = 32f;
            Item.value = Item.sellPrice(gold: 10);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.MeleeNoSpeed;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}