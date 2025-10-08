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
    public class SwordOfTheUniverseV4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Universe");
            /* Tooltip.SetDefault("'This sword doesn't swing. It lifts the Universe towards the blade'"
			    + "\nHas changeable forms"); */
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 275;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1 with { Pitch = -0.5f };
            Item.shoot = ModContent.ProjectileType<SOTUV4Projectile>();  
            Item.shootSpeed = 5f;
            Item.value = Item.sellPrice(platinum: 5);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
            Item.GetGlobalItem<ReflectionChance>().reflectChance = 10;
            Item.noMelee = true;
            Item.channel = true;
        }

        public override void HoldItem(Player player)
        {
            Item.noUseGraphic = player.ItemAnimationActive;
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -72f), Vector2.UnitY * 12f);
            }
        }

        public override bool MeleePrefix() => true;

        public override bool CanShoot(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Held.SwordOfTheUniverseV4>()] < 1;

        int swingDirection = 1;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swingDirection *= -1;
            Projectile.NewProjectile(source, position, Vector2.Normalize(velocity), ModContent.ProjectileType<Projectiles.Held.SwordOfTheUniverseV4>(), damage, knockback, player.whoAmI, ai1: swingDirection);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
            target.AddBuff(ModContent.BuffType<SuperVenom>(), 360);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV2");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV9");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverse");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV3");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV5");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV6");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV7");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV8");
            recipe.Register();
        }
    }
}