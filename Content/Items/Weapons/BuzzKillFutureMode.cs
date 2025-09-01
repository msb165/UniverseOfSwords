using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BuzzKillFutureMode : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Buzz Kill Future Mode");
            // Tooltip.SetDefault("'Release the Gamma ray infused bees!'");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.3F;
            Item.rare = ItemRarityID.Red;
            Item.crit = 4;
            Item.useStyle = 1;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 50;
            Item.knockBack = 1.0F;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.Beenade;
            Item.shootSpeed = 9;
            Item.value = Item.sellPrice(gold: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 10f * 0.0174f; //Replace 45 with whatever spread you want
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 2f;
            double offsetAngle;
            int i;
            for (i = 0; i < 7; i++) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "LunarOrb", 1);
            recipe.AddIngredient(ItemID.HiveBackpack, 1);
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddIngredient(null, "BuzzKill", 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}