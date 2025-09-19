using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MagicSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("You're wizard!");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.damage = 27;
            Item.knockBack = 8.55f;
            Item.UseSound = SoundID.Item109 with { Volume = 0.3f };
            Item.value = Item.sellPrice(gold: 2);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 12f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<SuperBeam>(), (int)(damageDone * 1.75), hit.Knockback, player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Starfury)
                .AddIngredient(ModContent.ItemType<MasterSword>())
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddIngredient(ItemID.MagicMissile)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 150)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}