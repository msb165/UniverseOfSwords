using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Biter : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("It's sharp!");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 32;
            Item.height = 32;			
            Item.rare = ItemRarityID.Orange;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 17;
            Item.useAnimation = 17;           
            Item.damage = 26;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 28);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
    }
}