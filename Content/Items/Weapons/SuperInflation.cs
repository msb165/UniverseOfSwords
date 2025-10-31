using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SuperInflation : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.5f;
            // Tooltip.SetDefault("'Throw money at ALL your problems'");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.damage = 240;
            Item.shoot = ModContent.ProjectileType<GoldenCoin>();
            Item.shootSpeed = 3.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 0;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void AddRecipes()
        {
            Mod thorium = UniverseOfSwords.Instance.ThoriumMod;
            Mod calamity = UniverseOfSwords.Instance.CalamityMod;
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Inflation>());
            recipe.AddIngredient(ModContent.ItemType<CopperCoinSword>());
            recipe.AddIngredient(ModContent.ItemType<SilverCoinSword>());
            recipe.AddIngredient(ModContent.ItemType<GoldCoinSword>());
            if (thorium is not null)
            {
                recipe.AddIngredient(thorium.Find<ModItem>("InfernoEssence"), 5);
                recipe.AddIngredient(thorium.Find<ModItem>("DeathEssence"), 5);
                recipe.AddIngredient(thorium.Find<ModItem>("OceanEssence"), 5);
            }
            if (calamity is not null)
            {
                recipe.AddIngredient(calamity.Find<ModItem>("Necroplasm"), 5);
            }
            recipe.AddIngredient(ModContent.ItemType<UpgradeMatter>(), 250);
            recipe.AddIngredient(ItemID.LunarOre);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float piOverTen = MathHelper.ToRadians(5f);
            for (int i = 0; i < 10; i++)
            {
                float offset = i - (10f - 1f) / 2f;
                Projectile.NewProjectileDirect(source, position + velocity, velocity.RotatedBy(piOverTen * offset), type, damage / 5, knockback, player.whoAmI, ai1: 1f);
            }

            return false;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360); // 6 seconds
        }
    }
}