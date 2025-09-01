using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MagicSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("You're wizard!");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = 1;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 35;
            Item.knockBack = 8.55f;
            Item.UseSound = new SoundStyle($"{UniverseUtils.SoundsPath}Item/Spell");
            Item.shoot = ProjectileID.EnchantedBeam;
            Item.shootSpeed = 10;
            Item.value = 110000;
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
            recipe.AddIngredient(null, "TheForce", 1);
            recipe.AddIngredient(ItemID.MagicMissile, 1);
            recipe.AddIngredient(null, "SwordMatter", 100);
            recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}