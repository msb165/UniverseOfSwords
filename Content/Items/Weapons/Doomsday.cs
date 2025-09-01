using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Doomsday : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 66;
            Item.height = 70; 
			Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;   
            Item.useAnimation = 20; 			
            Item.damage = 100; 
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item45;
            Item.value = 470000;		
            Item.shoot = ProjectileID.InfernoFriendlyBlast;
            Item.shootSpeed = 10;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
	}
}