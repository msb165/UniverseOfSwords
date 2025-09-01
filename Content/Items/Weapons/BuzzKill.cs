using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BuzzKill : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Buzz Kill");
            // Tooltip.SetDefault("'Release the Africanized bees!'");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.3F;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 30;
            Item.knockBack = 1f;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.GiantBee;
            Item.shootSpeed = 8;
            Item.value = Item.sellPrice(gold: 5);
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
            for (int i = 0; i < 3; i++) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), player.beeType(), damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BeeKeeper, 1);
            recipe.AddIngredient(ItemID.BeeGun, 1);
            recipe.AddIngredient(ItemID.Beenade, 80);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.HoneyBlock, 500);
            recipe.AddIngredient(ItemID.Hive, 500);
            recipe.AddIngredient(ModContent.ItemType<TheSwarm>(), 1);
            recipe.AddTile(TileID.HoneyDispenser);
            recipe.Register();
        }
    }
}