using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MusketBulletSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots Musket bullets");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 15;
            Item.knockBack = 3.5f;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.sellPrice(silver: 15);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MusketBall, 1000)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 100)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<FlyingMusket>()] >= 2 || !UniverseUtils.IsAValidTarget(target))
            {
                return;
            }

            Vector2 spawnDistance = Main.rand.NextVector2Unit() * new Vector2(200f) * Main.rand.NextFloat(0.3f, 1f);
            Vector2 spawnPos = target.Center - spawnDistance;
            for (int i = 0; i < 10; i++)
            {
                spawnDistance = Main.rand.NextVector2Unit() * new Vector2(200f) * Main.rand.NextFloat(0.3f, 1f);
                spawnPos = target.Center - spawnDistance;
                if (!Collision.SolidTiles(spawnPos, 16, 16))
                {
                    break;
                }
            }

            Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, Vector2.Zero, ModContent.ProjectileType<FlyingMusket>(), Item.damage, Item.knockBack, player.whoAmI, target.whoAmI);
        }
    }
}
