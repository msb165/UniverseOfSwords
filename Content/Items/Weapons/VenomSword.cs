using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class VenomSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.9F;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 7.0F;
            Item.damage = 63;
            Item.shoot = ProjectileID.VenomFang;
            Item.shootSpeed = 40;
            Item.UseSound = SoundID.Item43;
            Item.value = 200000;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(4); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(10f);
            position += Vector2.Normalize(velocity) * 5f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PoisonSword>(), 1);
            recipe.AddIngredient(ItemID.PoisonStaff, 1);
            recipe.AddIngredient(ModContent.ItemType<UpgradeMatter>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}