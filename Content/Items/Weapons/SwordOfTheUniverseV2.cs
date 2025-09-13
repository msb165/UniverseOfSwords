using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SwordOfTheUniverseV2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Universe");
            /* Tooltip.SetDefault("'This sword doesn't swing. It lifts the Universe towards the blade'"
                + "\nHas changeable forms"); */
        }

        public override void SetDefaults()
        {
            Item.width = 140;
            Item.height = 140;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 190;
            Item.knockBack = 20f;
            Item.UseSound = SoundID.Item1 with { Pitch = -0.5f };
            Item.shoot = ModContent.ProjectileType<SOTUProjectile2>();
            Item.shootSpeed = 5f;
            Item.value = Item.sellPrice(platinum: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.RainbowMk2, 2f, 32, 200, alpha: 0, Main.DiscoColor);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TrueHorrormageddon>())
                .AddIngredient(ModContent.ItemType<PrismSword>())
                .AddIngredient(ModContent.ItemType<EdgeLord>())
                .AddIngredient(ModContent.ItemType<SuperInflation>())
                .AddIngredient(ModContent.ItemType<CosmoStorm>())
                .AddIngredient(ModContent.ItemType<GlacialCracker>())
                .AddIngredient(ItemID.Arkhalis, 1)
                .AddTile(TileID.DemonAltar)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TrueHorrormageddon>())
                .AddIngredient(ModContent.ItemType<PrismSword>())
                .AddIngredient(ModContent.ItemType<EdgeLord>())
                .AddIngredient(ModContent.ItemType<SuperInflation>())
                .AddIngredient(ModContent.ItemType<CosmoStorm>())
                .AddIngredient(ModContent.ItemType<GlacialCracker>())
                .AddIngredient(ItemID.Terragrim, 1)
                .AddTile(TileID.DemonAltar)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverse>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV9>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV3>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV4>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV5>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV6>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV7>())
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV8>())
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<RainbowProj>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.Ichor, 360);
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.OnFire, 360);
            target.AddBuff(BuffID.Poisoned, 360);
            target.AddBuff(BuffID.CursedInferno, 360);
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
            for (int i = 0; i < 3; i++)
            {
                Vector2 spawnPos = player.Center + Main.rand.NextVector2Circular(300f, 300f);
                Vector2 newVel = (target.Center - spawnPos).SafeNormalize(Vector2.Zero) * 8f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, newVel, ModContent.ProjectileType<RainbowProj>(), hit.Damage, hit.Knockback, player.whoAmI);
            }
        }
    }
}