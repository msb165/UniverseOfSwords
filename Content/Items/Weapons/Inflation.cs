using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Utilities;

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
            Item.Size = new(64);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10f;
            Item.useTime = 62;
            Item.useAnimation = 62;
            Item.damage = 240;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 2000);
            Item.autoReuse = true;
            Item.holdStyle = 0;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                Dust dust = Dust.NewDustDirect(player.itemLocation - new Vector2(-38f * player.direction, 80f), 64, 64, DustID.GoldCoin, 0, 0, 127, default, 1f);
                if (player.direction == -1)
                {
                    dust.position.X -= 48f;
                }
                dust.noGravity = true;
                dust.velocity.X = Main.rand.Next(1, 10) * 0.5f * player.direction;
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), Vector2.Zero);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360); // 6 second
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldCoin, 2000)
                .AddIngredient(ItemID.GoldenCrate, 10)
                .AddIngredient(ItemID.GoldBrick, 999)
                .AddIngredient(ItemID.GoldBroadsword, 10)
                .AddIngredient(ItemID.GoldBar, 500)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.GoldCoin, 2000)
                .AddIngredient(ItemID.GoldenCrateHard, 10)
                .AddIngredient(ItemID.PlatinumBrick, 999)
                .AddIngredient(ItemID.PlatinumBroadsword, 10)
                .AddIngredient(ItemID.PlatinumBar, 500)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}