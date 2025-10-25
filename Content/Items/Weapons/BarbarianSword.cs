using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BarbarianSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'We gonna need more Barbarians'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 58;
            Item.height = 58; 
			Item.scale = 1.125f;
            Item.rare = ItemRarityID.Blue;            
            Item.useStyle = ItemUseStyleID.HoldUp;             
            Item.useTime = 24;
            Item.useAnimation = 24;           
            Item.damage = 20; 
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 15);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}	
    }
}