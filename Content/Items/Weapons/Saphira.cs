using Microsoft.Xna.Framework;
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
    public class Saphira : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Saphira");
            // Tooltip.SetDefault("'Sword with many shining sapphires attached to it'");
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.scale = 1.3f;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 60;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item46;
            Item.value = Item.sellPrice(gold: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numProjectiles = 4 + Main.rand.Next(4);
            for (int i = 0; i < numProjectiles; i++)
            {
                Vector2 targetPos = new((float)(player.Center.X + Main.rand.Next(100) * -player.direction + (Main.MouseWorld.X - player.position.X)), player.Center.Y - 600f);   //This defines the projectile width, direction and position.
                targetPos.X = (targetPos.X + player.Center.X) / 2f + Main.rand.Next(-100, 100);
                targetPos.Y -= 100 * i;
                Vector2 targetSpeed = (Main.MouseWorld - targetPos).SafeNormalize(Vector2.UnitY) * 16f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), targetPos, targetSpeed, ModContent.ProjectileType<SaphiraProj>(), Item.damage, Item.knockBack, player.whoAmI, ai1: Main.rand.Next(5));
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SapphireSword>())
                .AddIngredient(ItemID.SpookyWood, 400)
                .AddIngredient(ItemID.ScarecrowBanner)
                .AddIngredient(ItemID.ZombieElfBanner)
                .AddIngredient(ItemID.BlizzardStaff)
                .AddIngredient(ModContent.ItemType<BlizzardRage>())
                .AddIngredient(ModContent.ItemType<Orichalcon>(), 2)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 250)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}