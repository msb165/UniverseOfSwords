using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DirtSword : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 24;
            Item.height = 24; 
			Item.scale = 1.125f;
            Item.rare = ItemRarityID.White;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 6; 
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 15;			
            Item.autoReuse = false; 
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -55f), Vector2.UnitY * 4f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 25)
                .AddTile(TileID.WorkBenches)
                .Register();
	    }
    }
}
