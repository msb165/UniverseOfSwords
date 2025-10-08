using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
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
            Item.scale = 1.3f;
            Item.rare = ItemRarityID.Purple;
            Item.crit = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 440;
            Item.knockBack = 9f;
            Item.UseSound = SoundID.Item46;
            Item.value = Item.sellPrice(gold: 50);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                for (int i = 0; i < 3 + Main.rand.Next(3); i++)
                {
                    Vector2 spawnPos = player.Center + new Vector2(Main.rand.Next(-600, 601), -600f);
                    spawnPos.Y -= 50 * i;
                    Vector2 spawnVel = target.Center - spawnPos;
                    if (spawnVel.Y < 0f)
                    {
                        spawnVel.Y *= -1f;
                    }
                    if (spawnVel.Y < 20f)
                    {
                        spawnVel.Y = 20f;
                    }
                    spawnVel = Vector2.Normalize(spawnVel) * 10f;
                    Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, spawnVel, ProjectileID.Meteor1, Item.damage, Item.knockBack, player.whoAmI, 0f, Main.rand.NextFloat(1f, 2f));
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StarWrath)
                .AddIngredient(ModContent.ItemType<Saphira>())
                .AddIngredient(ItemID.FragmentSolar, 30)
                .AddIngredient(ItemID.FragmentVortex, 30)
                .AddIngredient(ItemID.FragmentNebula, 30)
                .AddIngredient(ItemID.FragmentStardust, 30)
                .AddIngredient(ItemID.MeteorStaff)
                .AddIngredient(ItemID.MeteoriteBar, 100)
                .AddIngredient(ItemID.HellstoneBar, 100)
                .AddIngredient(null, "Orichalcon", 10)
                .AddIngredient(ItemID.LunarBar, 50)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 1500)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}