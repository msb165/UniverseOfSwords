using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class ClusterBOOMer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cluster BOOMer");
            // Tooltip.SetDefault("'We do not take any responsibility for destroyed buildings'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(114);
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 2;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 75;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item62;
            Item.value = Item.sellPrice(gold: 20);
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(36f * player.direction, -24f), Vector2.UnitY * 30f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center + player.itemRotation.ToRotationVector2();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                Vector2 velocity = (target.Center - player.Center).SafeNormalize(-Vector2.UnitY) * 12f * (0.6f + Main.rand.NextFloat() * 0.8f);
                Vector2 spawnPos = player.Center + Utils.RandomVector2(Main.rand, -15f, 15f);
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), spawnPos + velocity, velocity, ModContent.ProjectileType<Rocket>(), Item.damage, Item.knockBack, player.whoAmI);
            }
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ModContent.ProjectileType<SuperExplosion>(), Item.damage, Item.knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ProximityMineLauncher)
                .AddIngredient(ItemID.AmmoBox)
                .AddIngredient(ItemID.GrenadeLauncher)
                .AddIngredient(ItemID.RocketLauncher)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 25)
                .AddIngredient(ItemID.RocketI, 2000)
                .AddIngredient(ItemID.RocketIII, 2000)
                .AddIngredient(ItemID.ClusterRocketI, 1000)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}