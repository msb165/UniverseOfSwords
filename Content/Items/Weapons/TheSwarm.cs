using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TheSwarm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 34;
            Item.useAnimation = 17;
            Item.damage = 15;
            Item.knockBack = 5.0F;
            Item.shoot = ProjectileID.Bee;
            Item.shootSpeed = 10;
            Item.UseSound = SoundID.Item1;
            Item.value = 38500;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 100);
            recipe.AddIngredient(ModContent.ItemType<TheStinger>(), 1);
            recipe.AddIngredient(ItemID.BeeGun, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}