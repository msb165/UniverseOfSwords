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
    public class GrandPiano : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grand Piano");
            // Tooltip.SetDefault("'Rage Quit - Horrior'");
        }

        public override void SetDefaults()
        {
            Item.width = 142;
            Item.height = 142;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), Vector2.Zero);
            }
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LivingWoodPiano, 1)
                .AddIngredient(ItemID.CactusPiano, 1)
                .AddIngredient(ItemID.EbonwoodPiano, 1)
                .AddIngredient(ItemID.RichMahoganyPiano, 1)
                .AddIngredient(ItemID.PalmWoodPiano, 1)
                .AddIngredient(ItemID.BorealWoodPiano, 1)
                .AddIngredient(ItemID.Piano, 1)
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