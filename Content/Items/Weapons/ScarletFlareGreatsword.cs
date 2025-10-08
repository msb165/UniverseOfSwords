using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Items.Placeable;
using UniverseOfSwords.Content.Projectiles.Common;

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
            Item.Size = new(120);
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 25;
            Item.damage = 124;
            Item.knockBack = 8f;
			Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.ScarletFlareGreatsword>();
            Item.value = Item.sellPrice(gold: 50);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        public override bool MeleePrefix() => true;
		
		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordShard>(), 15)
                .AddIngredient(ModContent.ItemType<RedFlareLongsword>(), 1)
                .AddIngredient(ModContent.ItemType<ScarletFlareCore>())
                .AddIngredient(ModContent.ItemType<TheNightmareAmalgamation>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
