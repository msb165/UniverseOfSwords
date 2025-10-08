using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TrueRuneBlade : ModItem
    {
        int swingDirection = 1;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Rune Blade");
            // Tooltip.SetDefault("'Pulses with strong energy of all rune elements'");
        }

        public override void SetDefaults()
        {
            Item.width = 68;
            Item.height = 68;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 60;
            Item.knockBack = 7f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.TrueRuneBlade>();
            Item.shootSpeed = 1f;
            Item.value = Item.sellPrice(gold: 6);
            Item.expert = true;
            Item.autoReuse = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FreezeFireClaw>())
                .AddIngredient(ItemID.CursedFlame, 30)
                .AddIngredient(ItemID.Ichor, 30)
                .AddIngredient(ItemID.ShadowFlameKnife)
                .AddIngredient(ItemID.DripplerBanner)
                .AddIngredient(ItemID.BloodZombieBanner)
                .AddIngredient(ModContent.ItemType<Orichalcon>())
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 15)
                .AddIngredient(ItemID.BrokenHeroSword)
                .AddTile(TileID.CrystalBall)
                .Register();
        }

        public override bool MeleePrefix() => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swingDirection *= -1;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai1: swingDirection);
            return false;
        }
    }
}