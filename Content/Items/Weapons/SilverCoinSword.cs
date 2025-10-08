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
    public class SilverCoinSword : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 58;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 5;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.sellPrice(silver: 99);
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
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - spawnPos, spawnVel, ModContent.ProjectileType<SilverCoin>(), Item.damage, Item.knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverCoin, 99)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 20)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}