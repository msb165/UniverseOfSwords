using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BuzzKillFutureMode : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Buzz Kill Future Mode");
            // Tooltip.SetDefault("'Release the Gamma ray infused bees!'");
        }

        public override void SetDefaults()
        {
            Item.width = 128;
            Item.height = 128;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 50;
            Item.knockBack = 1.0F;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.Beenade;
            Item.shootSpeed = 9;
            Item.value = Item.sellPrice(gold: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.shootsEveryUse = true;
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
                float rotation = player.itemRotation - MathHelper.PiOver4 * player.gravDir;
                if (player.direction == -1)
                {
                    rotation -= MathHelper.PiOver2 * player.gravDir;
                }
                Dust dust = Dust.NewDustPerfect(player.Center + rotation.ToRotationVector2() * 140f * Item.scale, DustID.Vortex, Vector2.Zero, Alpha: 127, newColor: default, Scale: 1.25f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(8f, 16f);
                if (dust.velocity.Y > 0f)
                {
                    dust.velocity *= -1f;
                }
                dust.velocity = dust.velocity.RotatedBy(-player.itemRotation * player.gravDir);
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), new Vector2(0f, 4f));
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<BuzzFutureEnergy>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(null, "LunarOrb", 1)
                .AddIngredient(ItemID.HiveBackpack, 1)
                .AddIngredient(ItemID.LunarBar, 20)
                .AddIngredient(null, "BuzzKill", 1)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}