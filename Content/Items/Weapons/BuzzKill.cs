using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

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
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.3F;
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
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            return false;
        }

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