using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Held;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DeusExcalibur : ModItem
    {
        int swingDirection = 0;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The Sword of (My) Life and (Your) Death'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.crit = 10;
            Item.DamageType = DamageClass.Melee;
            Item.width = 88;
            Item.height = 88;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 12f;
            Item.value = Item.sellPrice(platinum: 1);
            Item.rare = ItemRarityID.Purple;
            Item.scale = 1.25f;
            Item.autoReuse = true;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.DeusExcalibur>();
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai1: swingDirection);
            swingDirection++;
            if (swingDirection > 2)
            {
                swingDirection = 0;
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<CrystalExcalibur>(), 1)
            .AddIngredient(ModContent.ItemType<LunarOrb>(), 1)
            .AddIngredient(ModContent.ItemType<Orichalcon>(), 5)
            .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}