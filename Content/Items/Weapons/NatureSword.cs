using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class NatureSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Sword made out of only pure ingredients given from Mother Nature'");
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 72;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 15;
            Item.knockBack = 6f;
            Item.shoot = ProjectileID.VilethornBase;
            Item.shootSpeed = 20f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(silver: 50);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Green, 1.5f);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            proj.DamageType = DamageClass.MeleeNoSpeed;
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                 .AddIngredient(ItemID.Vilethorn)
                 .AddIngredient(ItemID.Seed, 10)
                 .AddIngredient(ItemID.Daybloom, 5)
                 .AddIngredient(ItemID.DirtBlock, 100)
                 .AddIngredient(ModContent.ItemType<SwordMatter>(), 40)
                 .AddTile(TileID.Anvils)
                 .Register();
            CreateRecipe()
                .AddIngredient(ItemID.TheRottedFork)
                .AddIngredient(ItemID.Seed, 10)
                .AddIngredient(ItemID.Daybloom, 5)
                .AddIngredient(ItemID.DirtBlock, 100)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 40)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
