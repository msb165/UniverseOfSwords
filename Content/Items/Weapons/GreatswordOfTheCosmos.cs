using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GreatswordOfTheCosmos : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Greatsword of the Cosmos");
            // Tooltip.SetDefault("'Look, up in the sky! Is it a bird?! Is it a plane?! No, it's-- HOLY S***!'");
        }

        public override void SetDefaults()
        {
            Item.width = 100;
            Item.height = 100;
            Item.scale = 1.3F;
            Item.rare = 11;
            Item.crit = 6;
            Item.useStyle = 1;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 440;
            Item.knockBack = 9.0F;
            Item.UseSound = SoundID.Item46;
            Item.shoot = ProjectileID.Meteor1;
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(gold: 50);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 30 + Main.rand.Next(30);
            for (int index = 0; index < numberProjectiles; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)(player.position.X + player.width * 1.0 + Main.rand.Next(100) * -player.direction + (Main.mouseX + (double)Main.screenPosition.X - player.position.X)), (float)(player.position.Y + player.height * 0.5 - 600.0));   //This defines the projectile width, direction and position.
                vector2_1.X = (float)((vector2_1.X + (double)player.Center.X) / 2.0) + Main.rand.Next(-100, 100);
                vector2_1.Y -= 500 * index;
                float num12 = Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if ((double)num13 < 0.0) num13 *= -1f;
                if ((double)num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = Item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float velocityX = num16 + Main.rand.Next(-12, 10) * 0.200f;  //This defines the projectile X position speed and randomness.
                float velocityY = num17 + Main.rand.Next(-12, 10) * 0.200f;  //This defines the projectile Y position speed and randomness.
                Projectile.NewProjectile(source, vector2_1.X, vector2_1.Y, velocityX, velocityY, type, damage, knockback, player.whoAmI, 0f, Main.rand.NextFloat(1f, 2f));
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StarWrath, 1);
            recipe.AddIngredient(null, "Saphira", 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 30);
            recipe.AddIngredient(ItemID.FragmentVortex, 30);
            recipe.AddIngredient(ItemID.FragmentNebula, 30);
            recipe.AddIngredient(ItemID.FragmentStardust, 30);
            recipe.AddIngredient(null, "PowerOfTheGalactic", 1);
            recipe.AddIngredient(ItemID.MeteorStaff, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 100);
            recipe.AddIngredient(ItemID.HellstoneBar, 100);
            recipe.AddIngredient(null, "Orichalcon", 10);
            recipe.AddIngredient(ItemID.LunarBar, 50);
            recipe.AddIngredient(null, "SwordMatter", 2000);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}