using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class ClusterBOOMer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cluster BOOMer");
            // Tooltip.SetDefault("'We do not take any responsibility for destroyed buildings'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(114);
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 2;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 75;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item62;
            Item.value = Item.sellPrice(gold: 50);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ProximityMineLauncher, 1)
                .AddIngredient(ItemID.AmmoBox, 1)
                .AddIngredient(ItemID.GrenadeLauncher, 1)
                .AddIngredient(ItemID.RocketLauncher, 1)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 10)
                .AddIngredient(ItemID.RocketI, 2000)
                .AddIngredient(ItemID.RocketIII, 2000)
                .AddIngredient(ItemID.ClusterRocketI, 1000)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                Vector2 velocity = (target.Center - player.Center).SafeNormalize(-Vector2.UnitY) * 12f * (0.6f + Main.rand.NextFloat() * 0.8f);
                Vector2 spawnPos = player.Center + Utils.RandomVector2(Main.rand, -15f, 15f);
                int projType = Utils.SelectRandom(Main.rand, [ProjectileID.RocketI, ProjectileID.RocketIII, ProjectileID.RocketIV, ProjectileID.ClusterRocketI]);
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos + velocity, velocity, projType, Item.damage, Item.knockBack, player.whoAmI);
            }

            if (Main.rand.NextBool(20))
            {
                Vector2 velocity = (target.Center - player.Center).SafeNormalize(-Vector2.UnitY) * 12f;
                Vector2 spawnPos = player.Center;
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos + velocity * 8f, velocity, ProjectileID.MiniNukeRocketI, Item.damage, Item.knockBack, player.whoAmI);
            }

            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ModContent.ProjectileType<SuperExplosion>(), Item.damage, Item.knockBack, player.whoAmI);
        }
    }
}