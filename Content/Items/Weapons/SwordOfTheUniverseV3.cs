using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Common;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SwordOfTheUniverseV3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Universe");
            /* Tooltip.SetDefault("'This sword doesn't swing. It lifts the Universe towards the blade'"
                + "\nHas changeable forms"); */
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.33f;
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 275;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item1 with { Pitch = -0.5f };
            Item.value = Item.sellPrice(platinum: 3);
            Item.autoReuse = true;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -72f), Vector2.UnitY * 12f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV2");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV9");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverse");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV4");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV5");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV6");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV7");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV8");
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //Projectile.NewProjectile(source, position + velocity * 10f, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<SuperVenom>(), 360);
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
            for (int i = 0; i < 6; i++)
            {
                if (UniverseUtils.IsAValidTarget(target))
                {
                    Vector2 spawnPos = Main.rand.NextVector2Circular(200f, 200f);
                    Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center - spawnPos, Vector2.Normalize(spawnPos), ModContent.ProjectileType<RedBeamV3>(), Item.damage * 10, hit.Knockback, player.whoAmI);
                }
            }
        }
    }
}