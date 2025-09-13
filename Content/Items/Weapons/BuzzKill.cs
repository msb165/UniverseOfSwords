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
    public class BuzzKill : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Buzz Kill");
            // Tooltip.SetDefault("'Release the Africanized bees!'");
        }

        public override void SetDefaults()
        {
            Item.width = 82;
            Item.height = 82;
            Item.scale = 1f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 30;
            Item.knockBack = 1f;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 8;
            Item.value = Item.sellPrice(gold: 5);
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), new Vector2(0f, 4f));
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            float spin = MathHelper.ToRadians(3f);
            for (int i = 0; i < 3; i++)
            {
                float offset = i - (3f - 1f) / 2f;
                Vector2 newVel = ((Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitY) * 4f).RotatedBy(spin * offset) * Main.rand.NextFloat(0.5f, 1.25f);
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), player.Center + newVel * 8f, newVel, player.beeType(), Item.damage, Item.knockBack, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BeeKeeper, 1);
            recipe.AddIngredient(ItemID.BeeGun, 1);
            recipe.AddIngredient(ItemID.Beenade, 80);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.HoneyBlock, 500);
            recipe.AddIngredient(ItemID.Hive, 500);
            recipe.AddIngredient(ModContent.ItemType<TheSwarm>(), 1);
            recipe.AddTile(TileID.HoneyDispenser);
            recipe.Register();
        }
    }
}