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
    public class SwordOfTheMultiverse : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Multiverse");
            // Tooltip.SetDefault("'WARNING! Do NOT craft this Sword! Crafting it will break the game AND your sanity!'");
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 100;
            Item.height = 100;
            Item.rare = ItemRarityID.Expert;
            Item.crit = 65;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 80000;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item1 with { Pitch = -1f };
            Item.shoot = ModContent.ProjectileType<SOTM>();
            Item.shootSpeed = 30f;
            Item.expert = true;
            Item.value = Item.sellPrice(platinum: 90);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
            Item.GetGlobalItem<ReflectionChance>().reflectChance = 50;
            Item.ArmorPenetration = 500;
        }

        //public override bool CanShoot(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        //public override bool MeleePrefix() => true;

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center + new Vector2(12f * -player.direction, 0f);
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(64f * player.direction, -96f), new Vector2(12f * -player.direction, 14f));
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SOTM>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            Mod calamity = UniverseOfSwords.Instance.CalamityMod;
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverse>());
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverseV2>());
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverseV3>());
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverseV4>());
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverseV5>());
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverseV6>());
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverseV7>());
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverseV8>());
            recipe.AddIngredient(ModContent.ItemType<SwordOfTheUniverseV9>());
            if (calamity is not null)
            {
                recipe.AddIngredient(calamity.Find<ModItem>("ExoPrism"), 10);
                recipe.AddIngredient(calamity.Find<ModItem>("AshesofAnnihilation"), 15);
            }
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 1000);
        }
    }
}
