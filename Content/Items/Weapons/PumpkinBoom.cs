using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class PumpkinBoom : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 65;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.JackOLantern;
            Item.shootSpeed = 10;
            Item.value = 360500;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PumpkinSword>(), 1)
                .AddIngredient(ItemID.JackOLanternLauncher, 1)
                .AddIngredient(null, "Orichalcon", 1)
                .AddIngredient(null, "SwordMatter", 100)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}