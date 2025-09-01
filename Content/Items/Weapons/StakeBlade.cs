using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class StakeBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.damage = 75;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.Stake;
            Item.shootSpeed = 20;
            Item.value = 380500;
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
            recipe.AddIngredient(ItemID.StakeLauncher, 1);
            recipe.AddIngredient(null, "Orichalcon", 1);
            recipe.AddIngredient(null, "SwordMatter", 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}