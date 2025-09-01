using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Projectiles.Common;
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
                .AddIngredient(ItemID.HellstoneBar, 80)
                .AddIngredient(ItemID.LunarBar, 40)
                .AddIngredient(ModContent.ItemType<SwordShard>(), 3)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 30)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DraculaSword>())
                .AddIngredient(ItemID.ScourgeoftheCorruptor)
                .AddIngredient(ItemID.VampireBanner)
                .AddIngredient(ItemID.HellstoneBar, 80)
                .AddIngredient(ItemID.LunarBar, 40)
                .AddIngredient(ModContent.ItemType<SwordShard>(), 3)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 30)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}