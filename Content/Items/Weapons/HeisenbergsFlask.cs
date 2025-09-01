using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class HeisenbergsFlask : ModItem
    {
		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Heisenberg's Flask");
			// Tooltip.SetDefault("'Hablan de un tal Heisenberg que ahora controla el mercado'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 28;
            Item.height = 28; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Cyan;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;   
            Item.useAnimation = 20; 			
            Item.damage = 50; 
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item107;
            Item.value = Item.sellPrice(gold: 2);					
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }   


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }
    }
}