using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class StarSword : ModItem
    {
        public int shootCount = 0;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots Fallen Stars");
        }

        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.damage = 33;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<StarProj>();
            Item.shootSpeed = 3.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 48);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordMatter", 100);
            recipe.AddIngredient(ItemID.StarCannon, 1);
            recipe.AddIngredient(ItemID.FallenStar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 spawnPos = Main.rand.NextVector2Circular(200f, 200f);
            if (spawnPos.Y < 0f)
            {
                spawnPos.Y *= -1f;
            }
            Vector2 spawnVel = spawnPos.SafeNormalize(-Vector2.UnitY) * 6f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - spawnPos, spawnVel, ModContent.ProjectileType<GenericSlash>(), hit.Damage, Item.knockBack, player.whoAmI, ai1: (Color.Red.PackedValue), ai2: 100);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position + velocity * 7f, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}