using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Common;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SwordOfTheUniverse : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Universe");
            /* Tooltip.SetDefault("'This sword doesn't swing. It lifts the Universe towards the blade'"
			    + "\nHas changeable forms"); */
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 15;
            Item.damage = 275;
            Item.knockBack = 20f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(platinum: 5);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 6f;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
            Item.GetGlobalItem<ReflectionChance>().reflectChance = 10;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Green, 1.5f, 32, 200);
            }
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                for (int j = 0; j < 6; j++)
                {
                    float f = Main.rand.NextFloat() * MathHelper.TwoPi;
                    Vector2 spawnPos = position - Vector2.UnitY * 16f + f.ToRotationVector2() * Main.rand.NextFloat(20f, 61f);

                    if (Collision.SolidTiles(spawnPos, 24, 24))
                    {
                        continue;
                    }

                    Vector2 spawnVel = (Main.MouseWorld - spawnPos).SafeNormalize(Vector2.UnitY) * Item.shootSpeed;
                    Projectile.NewProjectile(source, spawnPos + spawnVel, spawnVel * Main.rand.NextFloat(0.5f, 1.25f), ModContent.ProjectileType<SOTUGreenBolt>(), (int)(damage * 2), knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
            target.AddBuff(ModContent.BuffType<SuperVenom>(), 360);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV2>())
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
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV9>())
                .Register();
        }
    }
}