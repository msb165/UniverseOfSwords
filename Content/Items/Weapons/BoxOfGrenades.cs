using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BoxOfGrenades : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Box of Grenades");
            // Tooltip.SetDefault("'Forget throwing grenades one at a time. Just throw a boxful at your enemies!'");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 55;
            Item.useAnimation = 55;
            Item.damage = 60;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            //Item.shoot = ProjectileID.Grenade;
            //Item.shootSpeed = 10f;
            Item.value = Item.sellPrice(gold: 24);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FireworksLauncher, 1);
            recipe.AddIngredient(ItemID.AmmoBox, 1);
            recipe.AddIngredient(ItemID.WoodenCrate, 1);
            recipe.AddIngredient(ItemID.FireworksBox, 99);
            recipe.AddIngredient(null, "GrenadeBlade", 1);
            recipe.AddIngredient(null, "UpgradeMatter", 10);
            recipe.AddIngredient(ItemID.Grenade, 2000);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 position = player.Center;
            IEntitySource source = target.GetSource_OnHit(target);

            for (int j = 0; j < 8; j++)
            {
                float f = Main.rand.NextFloat() * MathHelper.TwoPi;
                Vector2 spawnPos = position - Vector2.UnitY * 16f + f.ToRotationVector2() * Main.rand.NextFloat(20f, 61f);

                if (Collision.SolidTiles(spawnPos, 8, 8))
                {
                    f = Main.rand.NextFloat() * MathHelper.TwoPi;
                    spawnPos = position + f.ToRotationVector2() * Main.rand.NextFloat(20f, 61f);
                }

                Vector2 spawnVel = (Main.MouseWorld - spawnPos).SafeNormalize(Vector2.UnitY) * 10f;
                Projectile.NewProjectileDirect(source, spawnPos + spawnVel, spawnVel * Main.rand.NextFloat(0.9f, 1.26f), ModContent.ProjectileType<Grenade>(), Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}