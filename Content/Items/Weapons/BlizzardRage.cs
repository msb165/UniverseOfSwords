using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Formats.Asn1;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BlizzardRage : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 50;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 450500;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (NPCID.Sets.CountsAsCritter[target.type] || target.immortal || !target.active)
            {
                return;
            }

            int numberProjectiles = 4 + Main.rand.Next(4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 spawnPos = target.Center + new Vector2(Main.rand.Next(-300, 301), -300f * Main.rand.NextFloat(0.9f, 2f));
                Vector2 spawnVel = Vector2.Normalize(target.Center - spawnPos) * 20f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, spawnVel, ProjectileID.NorthPoleSnowflake, (int)(Item.damage * 0.25f), Item.knockBack, player.whoAmI, ai1: Main.rand.Next(3));
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BlizzardStaff, 1);
            recipe.AddIngredient(null, "Orichalcon", 1);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}