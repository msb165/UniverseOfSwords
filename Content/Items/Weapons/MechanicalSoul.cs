using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MechanicalSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("Left click to shoot multiple projectiles that deal lower damage"
                + "\nRight click to shoot big projectile that deals higher damage"); */
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.expert = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 80;
            Item.knockBack = 8f;
            Item.shootSpeed = 3.25f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 25);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Pink;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -60f), Vector2.UnitY * 6f);
            }
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 120;
                Item.useAnimation = 20;
                Item.knockBack = 8f;
                Item.crit = 6;
                Item.damage = 120;
                Item.shoot = ModContent.ProjectileType<Projectiles.Common.MechanicalSoul>();
            }
            else
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.knockBack = 4f;
                Item.crit = 0;
                Item.damage = 80;
                Item.shoot = ModContent.ProjectileType<Soul1>();
            }
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 velocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitY) * Item.shootSpeed;
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + velocity * 4f, velocity, Item.shoot, (int)(Item.damage * 1.25f), Item.knockBack, player.whoAmI);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    float f = Main.rand.NextFloat() * MathHelper.TwoPi;
                    Vector2 spawnPos = position - Vector2.UnitY * 16f + f.ToRotationVector2() * Main.rand.NextFloat(20f, 61f);

                    if (Collision.SolidTiles(spawnPos, 8, 8))
                    {
                        continue;
                    }

                    Vector2 spawnVel = (Main.MouseWorld - spawnPos).SafeNormalize(Vector2.UnitY) * Item.shootSpeed;
                    Projectile.NewProjectile(source, spawnPos + spawnVel, spawnVel * Main.rand.NextFloat(0.9f, 1.26f), Utils.SelectRandom(Main.rand, [ModContent.ProjectileType<Soul1>(), ModContent.ProjectileType<Soul2>(), ModContent.ProjectileType<Soul3>()]), damage / 3, knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                for (int i = 0; i < 3; i++)
                {
                    UniverseUtils.SpawnRotatedDust(player, Utils.SelectRandom(Main.rand, DustID.Clentaminator_Blue, DustID.Clentaminator_Red, DustID.Clentaminator_Green), dustScale: 1.25f, start: (int)(16 * Item.scale), end: (int)(90 * Item.scale));
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Nightlight>(), 1)
                .AddIngredient(ItemID.HallowedBar, 30)
                .AddIngredient(ItemID.SoulofMight, 15)
                .AddIngredient(ItemID.SoulofFright, 15)
                .AddIngredient(ItemID.SoulofSight, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}