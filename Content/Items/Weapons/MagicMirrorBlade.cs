using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MagicMirrorBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magic Mirror Blade");
            // Tooltip.SetDefault("'Magic Mirror and sword fused together'");
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 64;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.UseSound = SoundID.Item6;
            Item.value = Item.sellPrice(silver: 50);
            Item.autoReuse = false;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            // Each frame, make some dust
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.1f);
            }
            // This sets up the itemTime correctly.
            if (player.itemTime == 0)
            {
                player.itemTime = (int)(Item.useTime / PlayerLoader.UseTimeMultiplier(player, Item));
            }
            else if (player.itemTime == (int)(Item.useTime / PlayerLoader.UseTimeMultiplier(player, Item)) / 2)
            {
                // This code runs once halfway through the useTime of the item. You'll notice with magic mirrors you are still holding the item for a little bit after you've teleported.

                // Make dust 70 times for a cool effect.
                for (int d = 0; d < 70; d++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default, 1.5f);
                }
                player.RemoveAllGrapplingHooks();
                // The actual method that moves the player back to bed/spawn.
                player.Spawn(PlayerSpawnContext.RecallFromItem);
                // Make dust 70 times for a cool effect. This dust is the dust at the destination.
                for (int d = 0; d < 35; d++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MagicMirror)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 15)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}