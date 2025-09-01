using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Inflation : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Your greed knows no bounds, does it?'");
        }

        public override void SetDefaults()
        {
            Item.width = 128;
            Item.height = 128;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10f;
            Item.useTime = 62;
            Item.useAnimation = 62;
            Item.damage = 240;
            Item.UseSound = SoundID.Item1;
            Item.value = 999999;
            Item.autoReuse = true;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldCoin, 2000);
            recipe.AddIngredient(ItemID.GoldenCrate, 10);
            recipe.AddIngredient(ItemID.GoldBrick, 999);
            recipe.AddIngredient(ItemID.GoldBroadsword, 10);
            recipe.AddIngredient(ItemID.GoldBar, 500);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldCoin, 2000);
            recipe.AddIngredient(ItemID.GoldenCrate, 10);
            recipe.AddIngredient(ItemID.PlatinumBrick, 999);
            recipe.AddIngredient(ItemID.PlatinumBroadsword, 10);
            recipe.AddIngredient(ItemID.PlatinumBar, 500);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360); // 6 second
        }
    }
}