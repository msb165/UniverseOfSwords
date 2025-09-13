using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class EdgeLord : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Blood for the Blood God! Skulls for the skull throne! Milk for the Khorne flakes!'");
        }

        public override void SetDefaults()
        {
            
            Item.Size = new(128);
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.damage = 222;
            Item.knockBack = 11f;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.VampireKnife;
            Item.shootSpeed = 30f;
            Item.value = Item.sellPrice(gold: 8);
            Item.autoReuse = true;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<EdgeLordEnergy>(), Item.damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            return false;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DraculaSword>())
                .AddIngredient(ItemID.VampireKnives)
                .AddIngredient(ItemID.VampireBanner)
                .AddIngredient(ItemID.HellstoneBar, 40)
                .AddIngredient(ItemID.LunarBar, 30)
                .AddIngredient(ModContent.ItemType<SwordShard>(), 30)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 30)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DraculaSword>())
                .AddIngredient(ItemID.ScourgeoftheCorruptor)
                .AddIngredient(ItemID.VampireBanner)
                .AddIngredient(ItemID.HellstoneBar, 40)
                .AddIngredient(ItemID.LunarBar, 30)
                .AddIngredient(ModContent.ItemType<SwordShard>(), 30)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 30)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}