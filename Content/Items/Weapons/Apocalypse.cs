using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Apocalypse : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Weapon that causes apocalypse");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 68;
            Item.scale = 1.3f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 45;
            Item.useAnimation = 15;
            Item.damage = 150;
            Item.knockBack = 12.0F;
            Item.UseSound = SoundID.Item116;
            Item.shoot = ProjectileID.ApprenticeStaffT3Shot;
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(gold: 30);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(2) == 0)
            {

                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 174, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 45f * 0.0174f; //Replace 45 with whatever spread you want
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 4f;
            double offsetAngle;
            for (int i = 0; i < 5; i++) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ApprenticeStaffT3, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddIngredient(null, "MartianSaucerCore", 1);
            recipe.AddIngredient(null, "SwordShard", 3);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 500);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}