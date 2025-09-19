using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class DutchmanSword : ModItem
    {
        public override void SetDefaults()
        { 
            Item.Size = new(64);
			Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightPurple;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 25;
            Item.useAnimation = 25;           
            Item.damage = 40; 
            Item.knockBack = 7.75f;
            Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileID.CannonballFriendly;
            Item.shootSpeed = 20f;
            Item.value = Item.sellPrice(gold: 10);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }
    }
}