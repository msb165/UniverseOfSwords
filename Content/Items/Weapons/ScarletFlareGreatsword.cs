using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class ScarletFlareGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scarlet Flare Greatsword");
            // Tooltip.SetDefault("Fires scarlet flare waves and ignites enemies with flames");
        }

        public override void SetDefaults()
        {
            Item.Size = new(60);
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 25;
            Item.damage = 124;
            Item.knockBack = 8f;
			Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.ScarletFlareGreatsword>();
            Item.value = Item.sellPrice(gold: 20);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -58f), Vector2.UnitY * 12f);
            }
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        public override bool MeleePrefix() => true;
		
		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordShard>(), 15)
                .AddIngredient(ModContent.ItemType<RedFlareLongsword>())
                .AddIngredient(ModContent.ItemType<ScarletFlareCore>())
                .AddIngredient(ModContent.ItemType<TheNightmareAmalgamation>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
