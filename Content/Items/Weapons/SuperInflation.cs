using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SuperInflation : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Throw money at ALL your problems'");
        }

        public override void SetDefaults()
        {
            Item.width = 128;
            Item.height = 128;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10f;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.damage = 240;
            Item.shoot = ModContent.ProjectileType<GoldenCoin>();
            Item.shootSpeed = 3.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 999999;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Inflation", 1);
            recipe.AddIngredient(null, "CopperCoinSword", 1);
            recipe.AddIngredient(null, "SilverCoinSword", 1);
            recipe.AddIngredient(null, "GoldCoinSword", 1);
            recipe.AddIngredient(null, "UpgradeMatter", 2);
            recipe.AddIngredient(ItemID.LunarOre, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float piOverTen = MathHelper.ToRadians(5f);
            for (int i = 0; i < 10; i++)
            {
                float offset = i - (10f - 1f) / 2f;
                Projectile.NewProjectileDirect(source, position + velocity, velocity.RotatedBy(piOverTen * offset), type, damage / 5, knockback, player.whoAmI);
            }

            return false;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360); // 6 seconds
        }
    }
}