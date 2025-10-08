using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SweetCornSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.33f;
            // Tooltip.SetDefault("Shoots Candy Corn");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.damage = 44;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 35);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 8f;
            Projectile.NewProjectileDirect(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<Corn>(), hit.Damage, hit.Knockback, player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CandyCornRifle, 1)
                .AddIngredient(null, "Orichalcon", 1)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 150)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}