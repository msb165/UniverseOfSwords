using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class StarSword : ModItem
    {
        public int shootCount = 0;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots Fallen Stars");
        }

        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 50;
            Item.useAnimation = 20;
            Item.damage = 33;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<StarProj>();
            Item.shootSpeed = 3.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 48);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 100)
                .AddIngredient(ItemID.StarCannon, 1)
                .AddIngredient(ItemID.FallenStar, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.MediumPurple, player.whoAmI, damageDone, 100);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position + velocity * 7f, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}