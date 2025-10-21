using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalItems;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SwordOfTheUniverseV8 : ModItem
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
            Item.width = 70;
            Item.height = 70;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Purple;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 275;
            Item.knockBack = 20f;
            Item.UseSound = SoundID.Item1 with { Pitch = -0.5f };
            Item.value = Item.sellPrice(platinum: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.GetGlobalItem<ReflectionChance>().reflectChance = 10;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 1f;
        }

        public override bool MeleePrefix() => true;

        public override bool CanShoot(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Held.SwordOfTheUniverseV8>()] < 1;

        int swingDirection = 1;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swingDirection *= -1;
            Projectile.NewProjectile(source, position, Vector2.Normalize(velocity), ModContent.ProjectileType<Projectiles.Held.SwordOfTheUniverseV8>(), damage, knockback, player.whoAmI, ai1: swingDirection);
            return false;
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
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverse>())
                .Register();
        }
    }
}