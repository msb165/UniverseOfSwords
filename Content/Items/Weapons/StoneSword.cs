using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class StoneSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'You Rock!'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32; 
			Item.scale = 1f;
            Item.rare = ItemRarityID.Blue;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 7; 
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 100;
            Item.autoReuse = false; 
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
        
		public override void AddRecipes()
	    {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 20)
                .AddTile(TileID.WorkBenches)
                .Register();
	    }
    }
}
