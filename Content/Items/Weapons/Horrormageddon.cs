using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Horrormageddon : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.75f;
            // Tooltip.SetDefault("'Where you see an army, I see a graveyard'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(128);
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 180;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item71;
            Item.shoot = ProjectileID.DeathSickle;
            Item.shootSpeed = 10f;
            Item.value = Item.sellPrice(gold: 6, silver: 6, copper: 6);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
            Item.noMelee = true;
            Item.shootsEveryUse = true;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                Vector2 spawnVel = Main.rand.NextVector2CircularEdge(200f, 200f);
                Vector2 spawnPos = player.Center - spawnVel;
                Dust dust = Dust.NewDustPerfect(spawnPos, DustID.Clentaminator_Green, Vector2.Zero);
                dust.position = spawnPos;
                dust.scale = 1f;
                dust.velocity = -Vector2.Normalize(dust.position - player.Center) * 8f;
                dust.noGravity = true;
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -60f), Vector2.UnitY * 6f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            base.MeleeEffects(player, hitbox);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Doomsday>())
                .AddIngredient(ModContent.ItemType<StarMaelstorm>())
                .AddIngredient(ModContent.ItemType<InnosWrath>())
                .AddIngredient(ModContent.ItemType<BeliarClaw>())
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 25)
                .AddIngredient(ModContent.ItemType<LunarOrb>())
                .AddIngredient(ItemID.LargeEmerald)
                .AddIngredient(ItemID.Meowmere)
                .AddIngredient(ItemID.TheHorsemansBlade)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<HorrormageddonEnergy>(), Item.damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            return false;
        }
    }
}