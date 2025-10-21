using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SwordOfTheUniverseV6 : ModItem
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
            Item.width = 80;
            Item.height = 80; 
			Item.scale = 1.1f;
            Item.rare = ItemRarityID.Purple;
            Item.crit = 16;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 275;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item1 with { Pitch = -0.5f };
            Item.shoot = ModContent.ProjectileType<SOTU7>();
            Item.shootSpeed = 4f;
            Item.value = Item.sellPrice(platinum: 5);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
            Item.GetGlobalItem<ReflectionChance>().reflectChance = 10;
            Item.noMelee = true;
            Item.channel = true;
            Item.noUseGraphic = true;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }


        public override bool MeleePrefix() => true;

        public override bool CanShoot(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Held.SwordOfTheUniverseV6>()] < 1;

        int swingDirection = 1;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swingDirection *= -1;
            Projectile.NewProjectile(source, position, Vector2.Normalize(velocity), ModContent.ProjectileType<Projectiles.Held.SwordOfTheUniverseV6>(), damage, knockback, player.whoAmI, ai1: swingDirection);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV2");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverse");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV9");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV3");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV4");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV5");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV7");
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SwordOfTheUniverseV8");
            recipe.Register();
        }
    }
}