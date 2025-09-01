using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MolotovSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The bottle cracked under the pressure and the flames came out of the wrong end. Though it seems to have improved it...'");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.7F;
            Item.rare = 4;
            Item.crit = 8;
            Item.useStyle = 1;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 69;
            Item.knockBack = 6.0F;
            Item.shoot = ProjectileID.MolotovCocktail;
            Item.shootSpeed = 12;
            Item.UseSound = new SoundStyle("Sounds/Item/Flare");
            Item.value = Item.sellPrice(gold: 20);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DamascusBar", 30);
            recipe.AddIngredient(ItemID.MolotovCocktail, 99);
            recipe.AddIngredient(ItemID.HellstoneBar, 50);
            recipe.AddIngredient(ItemID.LivingFireBlock, 100);
            recipe.AddIngredient(ItemID.Sake, 30);
            recipe.AddIngredient(ItemID.Flamethrower, 1);
            recipe.AddTile(TileID.Kegs);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 10f * 0.0174f; //Replace 45 with whatever spread you want
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 2f;
            double offsetAngle;
            int i;
            for (i = 0; i < 3; i++) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}