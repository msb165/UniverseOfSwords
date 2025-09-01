using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class ThunderOathSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Thunder Oath Sword");
            // Tooltip.SetDefault("'Most electrifying experience yet'");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.3F;
            Item.rare = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 30;
            Item.damage = 40;
            Item.knockBack = 4.0F;
            Item.UseSound = SoundID.Item92;
            Item.shoot = ProjectileID.MonkStaffT3_AltShot;
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(gold: 2);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RestoredHeroSword>());
            recipe.AddIngredient(ItemID.Starfury, 1);
            recipe.AddIngredient(ModContent.ItemType<UpgradeMatter>(), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}