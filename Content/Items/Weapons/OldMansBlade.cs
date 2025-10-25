using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class OldMansBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots Golden Shower");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.damage = 85;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<Ichor>();
            Item.shootSpeed = 10f;
            Item.value = Item.sellPrice(silver: 25);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 newVel = new(1f * player.direction, -4f);
            for (int i = 1; i <= 3; i++)
            {
                Projectile.NewProjectileDirect(source, position + velocity, newVel + (new Vector2(1f * player.direction, -2f) * (i * 0.125f)), type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 400);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AllWoodSword>());
            recipe.AddIngredient(ItemID.SpookyWood, 100);
            recipe.AddIngredient(ItemID.Pearlwood, 100);
            recipe.AddIngredient(ModContent.ItemType<Orichalcon>());
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}