using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class StarDestroyer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Each time you swing this sword stars are being shattered to pieces'");
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 100;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item88;
            Item.value = Item.sellPrice(gold: 2);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                for (int i = 0; i < 3; i++)
                {
                    UniverseUtils.SpawnRotatedDust(player, DustID.UnusedWhiteBluePurple, 2f, (int)(14 * Item.scale), (int)(84 * Item.scale));
                }
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numberProjectiles = 6 + Main.rand.Next(6);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 vector2_1 = new((float)(player.Center.X + Main.rand.Next(100) * -player.direction + (Main.MouseWorld.X - player.position.X)), (float)(player.Center.Y - 600f));   //This defines the projectile width, direction and position.
                vector2_1.X = (float)((vector2_1.X + (double)player.Center.X) / 2f) + Main.rand.Next(-100, 100);
                vector2_1.Y -= 100f * i;
                float num12 = Main.MouseWorld.X - vector2_1.X;
                float num13 = Main.MouseWorld.Y - vector2_1.Y;
                if (num13 < 0f)
                {
                    num13 *= -1f;
                }
                if (num13 < 20f)
                {
                    num13 = 20f;
                }
                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = 30f / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float velocityX = num16 + Main.rand.Next(-12, 10) * 0.160f;  //This defines the projectile X position speed and randomness.
                float velocityY = num17 + Main.rand.Next(-12, 10) * 0.160f;  //This defines the projectile Y position speed and randomness.
                Projectile proj = Projectile.NewProjectileDirect(target.GetSource_OnHit(target), vector2_1, new Vector2(velocityX, velocityY), ProjectileID.LunarFlare, Item.damage, Item.knockBack, player.whoAmI);
                proj.DamageType = DamageClass.Melee;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarFlareBook, 1);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "Orichalcon", 2);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddIngredient(null, "LunarOrb", 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}