using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SilverCoinSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots silver coins");
        }

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
            Item.shoot = ProjectileID.SilverCoin;
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(silver: 99);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 spawnPos = Main.rand.NextVector2CircularEdge(200f, 200f);
            Vector2 spawnVel = spawnPos.SafeNormalize(Vector2.UnitY) * 10f;
            if (Collision.SolidTiles(target.Center - spawnPos, 8, 8))
            {
                spawnPos = Main.rand.NextVector2CircularEdge(200f, 200f);
                spawnVel = spawnPos.SafeNormalize(Vector2.UnitY) * 10f;
            }
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - spawnPos, spawnVel, ProjectileID.SilverCoin, Item.damage, Item.knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SilverCoin, 99);
            recipe.AddIngredient(null, "SwordMatter", 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}