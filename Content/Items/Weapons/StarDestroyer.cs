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
    public class StarDestroyer : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0f;
            // Tooltip.SetDefault("'Each time you swing this sword stars are being shattered to pieces'");
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 100;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item88;
            Item.value = Item.sellPrice(gold: 2);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -56f));
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                for (int i = 0; i < 3; i++)
                {
                    UniverseUtils.SpawnRotatedDust(player, DustID.UnusedWhiteBluePurple, 2f, (int)(14 * Item.scale), (int)(84 * Item.scale));
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<StarDestroyerEnergy>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            return false;
        } 

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LunarFlareBook)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddIngredient(ModContent.ItemType<Orichalcon>(), 2)
                .AddIngredient(ItemID.FragmentSolar, 5)
                .AddIngredient(ItemID.FragmentVortex, 5)
                .AddIngredient(ItemID.FragmentNebula, 5)
                .AddIngredient(ItemID.FragmentStardust, 5)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 50)
                .AddIngredient(ModContent.ItemType<LunarOrb>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}