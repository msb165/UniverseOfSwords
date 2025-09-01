using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Sharkron : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Shaaark!");
		}
		
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;   
            Item.useAnimation = 20; 			
            Item.damage = 98; 
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = 440000;		
            Item.shoot = ProjectileID.MiniSharkron;
            Item.shootSpeed = 10f;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }   


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }
	}
}