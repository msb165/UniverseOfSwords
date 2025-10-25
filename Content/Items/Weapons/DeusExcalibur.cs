using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;

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
            Item.damage = 180;
            Item.crit = 10;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8f;
            Item.value = Item.sellPrice(platinum: 1);
            Item.rare = ItemRarityID.Purple;
            Item.scale = 1.25f;
            Item.autoReuse = true;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.DeusExcalibur>();
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.noUseGraphic = player.ItemAnimationActive;
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY);
            }
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
            .AddIngredient(ModContent.ItemType<CrystalExcalibur>())
            .AddIngredient(ModContent.ItemType<LunarOrb>())
            .AddIngredient(ModContent.ItemType<Orichalcon>(), 5)
            .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 250)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}