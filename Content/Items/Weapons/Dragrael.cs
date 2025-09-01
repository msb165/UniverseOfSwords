using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Dragrael : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Fused from all blades of dragon riders'");
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 58;
            Item.scale = 1.1F;
            Item.rare = 7;
            Item.useStyle = 1;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 124;
            Item.knockBack = 6.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 780000;
            Item.shoot = ProjectileID.TerraBeam;
            Item.shootSpeed = 20;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 74, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 182, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 111, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ZarRoc", 1);
            recipe.AddIngredient(null, "Tamerlein", 1);
            recipe.AddIngredient(null, "Brisingr", 1);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofSight, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.PixieDust, 25);
            recipe.AddIngredient(null, "SwordMatter", 150);
            recipe.AddIngredient(null, "Orichalcon", 1);
            recipe.AddIngredient(ItemID.TerraBlade, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 5f * 0.0174f; //Replace 45 with whatever spread you want
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 1f;
            double offsetAngle;
            int i;
            for (i = 0; i < 2; i++) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.ShadowFlame, 360);
            target.AddBuff(BuffID.Confused, 360);
            target.AddBuff(BuffID.OnFire, 360);
            target.AddBuff(BuffID.Frostburn, 360);
        }
    }
}
