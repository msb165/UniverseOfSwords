using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Placeable;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static Terraria.ModLoader.ModContent;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GemSlayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Inflicts Midas debuff on enemies");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.damage = 29;
            Item.knockBack = 5.7f;
            Item.UseSound = SoundID.Item1;
            Item.value = 20000;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemType<TopazSword>())
                .AddIngredient(ItemType<SapphireSword>())
                .AddIngredient(ItemType<EmeraldSword>())
                .AddIngredient(ItemType<AmethystSword>())
                .AddIngredient(ItemType<AmberSword>())
                .AddIngredient(ItemType<DiamondSword>())
                .AddIngredient(ItemType<RubySword>())
                .AddIngredient(ItemType<DamascusBar>(), 15)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360);
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 8f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ProjectileType<GemBolt>(), (int)(damageDone * 0.75), hit.Knockback, player.whoAmI, ai0: Main.rand.Next(6));
        }
    }
}
