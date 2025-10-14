using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Inflation : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Your greed knows no bounds, does it?'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(48);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10f;
            Item.useTime = 62;
            Item.useAnimation = 62;
            Item.damage = 90;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 2000);
            Item.autoReuse = true;
            Item.holdStyle = 0;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.Inflation>();
            Item.shootSpeed = 1f;
            Item.noMelee = true;
        }

        public override bool CanShoot(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        int swingDirection = 1;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swingDirection *= -1;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai1: swingDirection);
            return false;
        }

        public override void HoldItem(Player player)
        {
            Item.noUseGraphic = player.ItemAnimationActive;
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
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
                Dust dust = Dust.NewDustPerfect(player.Center + rotation.ToRotationVector2() * 20f * Item.scale, DustID.GoldCoin, Vector2.Zero, Alpha: 127, newColor: default, Scale: 3f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(4f, 8f);
                dust.velocity = dust.velocity.RotatedBy(-player.itemRotation * player.gravDir);
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), Vector2.UnitY * 6f);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360); // 6 second
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldCoin, 2000)
                .AddIngredient(ItemID.GoldenCrate, 10)
                .AddIngredient(ItemID.GoldBrick, 1000)
                .AddIngredient(ItemID.GoldBroadsword, 10)
                .AddIngredient(ItemID.GoldBar, 500)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.GoldCoin, 2000)
                .AddIngredient(ItemID.GoldenCrateHard, 10)
                .AddIngredient(ItemID.PlatinumBrick, 1000)
                .AddIngredient(ItemID.PlatinumBroadsword, 10)
                .AddIngredient(ItemID.PlatinumBar, 500)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}