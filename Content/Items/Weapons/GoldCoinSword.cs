using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class GoldCoinSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots gold coins");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 10;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.sellPrice(gold: 20);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -72f), Vector2.UnitX * 4f * player.direction);
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.GoldCoin, 1.25f, end: (int)(100 * Item.scale));
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 spawnPos = Main.rand.NextVector2CircularEdge(200f, 200f);
            Vector2 spawnVel = spawnPos.SafeNormalize(Vector2.UnitY) * 10f;
            for (int i = 0; i < 20; i++)
            {
                if (!Collision.SolidTiles(target.Center - spawnPos, 16, 16))
                {
                    break;
                }
                spawnPos = Main.rand.NextVector2CircularEdge(200f, 200f);
                spawnVel = spawnPos.SafeNormalize(Vector2.UnitY) * 10f;
            }
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - spawnPos, spawnVel, ModContent.ProjectileType<GoldenCoin>(), Item.damage, Item.knockBack / 2, player.whoAmI, ai1: 1f);
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Midas, 100);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldCoin, 20);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 50);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}