using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Common;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SwordOfTheUniverseV2 : ModItem
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
            Item.rare = ItemRarityID.Red;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 275;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1 with { Pitch = -0.5f };
            Item.value = Item.sellPrice(platinum: 5);
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

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
            target.AddBuff(ModContent.BuffType<SuperVenom>(), 360);
            for (int i = 0; i < 4; i++)
            {
                Vector2 spawnPos = player.Center + Main.rand.NextVector2Circular(300f, 300f);
                Vector2 newVel = (target.Center - spawnPos).SafeNormalize(Vector2.Zero) * 8f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, newVel, ModContent.ProjectileType<RainbowProj>(), hit.Damage * 2, hit.Knockback, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            Mod thorium = UniverseOfSwords.Instance.ThoriumMod;
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TrueHorrormageddon>());
            recipe.AddIngredient(ModContent.ItemType<PrismSword>());
            recipe.AddIngredient(ModContent.ItemType<EdgeLord>());
            recipe.AddIngredient(ModContent.ItemType<SuperInflation>());
            recipe.AddIngredient(ModContent.ItemType<GlacialCracker>());
            if (thorium is not null)
            {
                recipe.AddIngredient(thorium.Find<ModItem>("InfernoEssence"), 20);
                recipe.AddIngredient(thorium.Find<ModItem>("DeathEssence"), 20);
                recipe.AddIngredient(thorium.Find<ModItem>("OceanEssence"), 20);
            }
            recipe.AddIngredient(ItemID.Arkhalis);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<TrueHorrormageddon>());
            recipe2.AddIngredient(ModContent.ItemType<PrismSword>());
            recipe2.AddIngredient(ModContent.ItemType<EdgeLord>());
            recipe2.AddIngredient(ModContent.ItemType<SuperInflation>());
            recipe2.AddIngredient(ModContent.ItemType<GlacialCracker>());
            recipe2.AddIngredient(ItemID.Terragrim);
            recipe2.AddTile(TileID.LunarCraftingStation);
            recipe2.Register();

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
    }
}