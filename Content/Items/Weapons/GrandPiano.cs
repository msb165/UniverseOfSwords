using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class GrandPiano : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grand Piano");
            // Tooltip.SetDefault("'Rage Quit - Horrior'");
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Orange;
            Item.crit = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 100;
            Item.knockBack = 8f;
            Item.UseSound = new Terraria.Audio.SoundStyle($"{UniverseUtils.SoundsPath}Item/GrandPiano") with { Volume = 0.25f };
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 5f;
            Item.value = Item.sellPrice(gold: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), new Vector2(2f * player.direction, 4f));
            }
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LivingWoodPiano)
                .AddIngredient(ItemID.CactusPiano)
                .AddIngredient(ItemID.EbonwoodPiano)
                .AddIngredient(ItemID.RichMahoganyPiano)
                .AddIngredient(ItemID.PalmWoodPiano)
                .AddIngredient(ItemID.BorealWoodPiano)
                .AddIngredient(ItemID.Piano)
                .AddIngredient(ModContent.ItemType<PianoSword2>())
                .AddIngredient(ModContent.ItemType<PianoSword4>())
                .AddTile(TileID.Autohammer)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < Main.rand.Next(1, 5); i++)
            {
                Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.75f, 1.25f);
                Projectile.NewProjectile(source, position + newVel * 8f, newVel, ModContent.ProjectileType<Note>(), damage / 2, knockback, player.whoAmI, ai1: Main.rand.Next(0, 3), ai2: Main.rand.NextFloat(0.1f, 0.4f));
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 360);
            target.AddBuff(BuffID.Electrified, 360);
            target.AddBuff(BuffID.Bleeding, 360);
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.ShadowFlame, 360);
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.Slimed, 360);
            target.AddBuff(BuffID.Venom, 360);
        }
    }
}