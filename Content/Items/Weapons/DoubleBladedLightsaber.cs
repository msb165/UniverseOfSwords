using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Held;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons     
{
    public class DoubleBladedLightsaber : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Double Bladed Lightsaber");
			// Tooltip.SetDefault("'Watch out to not cut your body in half'");
		}
		
        public override void SetDefaults()
        {
			Item.damage = 65;
            Item.DamageType = DamageClass.MeleeNoSpeed;   
            Item.width = 70;  
            Item.height = 70;  
			Item.scale = 1f;
            Item.useTime = 10; 
            Item.useAnimation = 10;    
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Swing;    
            Item.knockBack = 8f; 
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Lime;                      
            Item.shoot = ModContent.ProjectileType<DoubleBladedLightsaberProjectile>();  
            Item.noUseGraphic = true; 
			Item.noMelee = true;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.noUseGraphic = player.ItemAnimationActive;
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override bool MeleePrefix() => true;

		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.YellowPhasesaber, 1)
                .AddIngredient(ItemID.WhitePhasesaber, 1)
                .AddIngredient(ItemID.PurplePhasesaber, 1)
                .AddIngredient(ItemID.GreenPhasesaber, 1)
                .AddIngredient(ItemID.BluePhasesaber, 1)
                .AddIngredient(ItemID.RedPhasesaber, 1)
                .AddIngredient(ItemID.SoulofFright, 12)
                .AddIngredient(ItemID.SoulofSight, 12)
                .AddIngredient(ItemID.SoulofMight, 12)
                .AddIngredient(ModContent.ItemType<HumanBuzzSaw>())
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 15)
                .AddIngredient(ItemID.CrystalShard, 50)
                .AddTile(TileID.MythrilAnvil)
                .Register();
	    } 
	}
}