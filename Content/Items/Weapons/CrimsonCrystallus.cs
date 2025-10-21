using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class CrimsonCrystallus : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 54;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 20;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<Tier2CProjectile>();
            Item.shootSpeed = 3f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -60f), Vector2.UnitY * 6f);
            }
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.Crimson, 1.5f, end: 90, alpha: 100);
            }
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += velocity * 4f - Vector2.UnitY * 24f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Crystallus>())
                .AddIngredient(ItemID.CrimtaneBar, 12)
                .AddIngredient(ItemID.TissueSample, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}