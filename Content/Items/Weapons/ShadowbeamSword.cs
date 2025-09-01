using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities.Terraria.Utilities;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class ShadowbeamSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 68;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item72;
            Item.shoot = ModContent.ProjectileType<PurpleLaserBeam>();
            Item.shootSpeed = 1f;
            Item.value = Item.sellPrice(gold: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float projAmount = 3;
            for (int i = 0; i < projAmount; i++) 
            {
                float offset = i - (projAmount - 1f) / 2f;
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Pi / 10f * offset);
                Projectile.NewProjectile(source, position + perturbedSpeed * 8f, perturbedSpeed, type, damage / 2, knockback, player.whoAmI); //create the projectile
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newPos = Main.rand.NextVector2Circular(200f, 200f);
            Vector2 newVel = newPos.SafeNormalize(Vector2.UnitY) * 4f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - newPos, newVel, Item.shoot, Item.damage / 2, Item.knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShadowbeamStaff, 1);
            recipe.AddIngredient(ModContent.ItemType<UpgradeMatter>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 130);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}