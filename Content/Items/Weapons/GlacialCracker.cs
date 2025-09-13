using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GlacialCracker : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'How did you manage to already break it?'");
        }

        public override void SetDefaults()
        {
            Item.width = 162;
            Item.height = 162;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.damage = 180;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item28;
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
                .AddIngredient(ItemID.IceBlade, 1)
                .AddIngredient(ItemID.Amarok, 1)
                .AddIngredient(ItemID.Frostbrand, 2)
                .AddIngredient(ItemID.NorthPole, 1)
                .AddIngredient(ItemID.FrostCore, 10)
                .AddIngredient(ItemID.IceFeather, 2)
                .AddIngredient(ModContent.ItemType<SwordShard>(), 5)
                .AddIngredient(ItemID.IceBlock, 1000)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numberProjectiles = 3 + Main.rand.Next(4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 targetPos = target.Center + new Vector2(Main.rand.Next(-400, 401), -300f * Main.rand.NextFloat(0.9f, 2f));   //This defines the projectile width, direction and position.
                Vector2 targetVelocity = Vector2.Normalize(target.Center - targetPos) * 15f;
                targetVelocity.X += Main.rand.Next(-12, 10) * 0.160f;
                targetVelocity.Y += Main.rand.Next(-12, 10) * 0.160f;
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), targetPos, targetVelocity, ProjectileID.NorthPoleSpear, Item.damage, Item.knockBack, player.whoAmI);
            }
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.Frostburn2, 360);
        }
    }
}