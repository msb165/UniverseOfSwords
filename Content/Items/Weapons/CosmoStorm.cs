using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class CosmoStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Sword that shatters galaxies'");
        }

        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35;
            Item.scale = 1.5F;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.damage = 180;
            Item.UseSound = SoundID.Item15;
            Item.shoot = ProjectileID.NebulaArcanum;
            Item.shootSpeed = 10f;
            Item.value = 650000;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(32f * player.direction, -24f), new Vector2(0f, 4f));
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentNebula, 80)
                .AddIngredient(ItemID.FragmentSolar, 80)
                .AddIngredient(ModContent.ItemType<LunarOrb>())
                .AddIngredient(ModContent.ItemType<PowerOfTheGalactic>())
                .AddIngredient(ItemID.LunarBar, 40)
                .AddIngredient(ItemID.PortalGun)
                .AddIngredient(ItemID.NebulaArcanum)
                .AddIngredient(ItemID.LargeAmethyst, 4)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 25)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 5f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 1f;
            double offsetAngle;
            for (int i = 0; i < 3; i++) 
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}