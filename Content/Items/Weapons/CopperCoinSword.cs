using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class CopperCoinSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots copper coins");
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 2;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.sellPrice(copper: 99);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.CopperCoin, 1.25f);
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
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - spawnPos, spawnVel, ProjectileID.CopperCoin, Item.damage, Item.knockBack, player.whoAmI);
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CopperCoin, 99);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}