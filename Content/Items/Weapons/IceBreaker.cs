using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class IceBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Freezing to the touch'");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.damage = 61;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item28 with { Volume = 0.6f };
            Item.value = 300200;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.noUseGraphic = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.holdStyle = 0;
            Item.GetGlobalItem<ReflectionChance>().reflectChance = 3;
        }

        public override bool CanUseItem(Player player)
        {
            Item.UseSound = player.altFunctionUse == 2 ? SoundID.Item92 : SoundID.Item28;
            return player.ownedProjectileCounts[ModContent.ProjectileType<IceBreakerProj>()] < 1;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, player.Center + new Vector2(24f * player.direction, -48f), Vector2.Zero, ModContent.ProjectileType<IceBreakerProj>(), damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void HoldItem(Player player)
        {
            Item.noUseGraphic = Item.noMelee = player.altFunctionUse == 2;
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle && player.ownedProjectileCounts[ModContent.ProjectileType<IceBreakerProj>()] < 1 ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle && player.ownedProjectileCounts[ModContent.ProjectileType<IceBreakerProj>()] < 1)
            {
                float rotation = player.itemRotation - MathHelper.PiOver4 * player.gravDir;
                if (player.direction == -1)
                {
                    rotation -= MathHelper.PiOver2 * player.gravDir;
                }
                Dust dust = Dust.NewDustPerfect(player.Center + rotation.ToRotationVector2() * 20f * Item.scale, DustID.SpectreStaff, Vector2.Zero, Alpha: 127, newColor: default, Scale: 2f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(4f, 16f);
                if (dust.velocity.Y > 0f)
                {
                    dust.velocity *= -1f;
                }
                dust.velocity = dust.velocity.RotatedBy(-player.itemRotation);
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), new Vector2(1f, 6f));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.altFunctionUse != 2 && UniverseUtils.IsAValidTarget(target))
            {
                float numberProjectiles = 1 + Main.rand.Next(3);
                Vector2 position = player.Center;
                Vector2 velocity = Vector2.Normalize(target.Center - player.Center) * 10f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Projectile.NewProjectile(target.GetSource_OnHit(target), position + velocity.RotatedByRandom(MathHelper.ToRadians(90f)), velocity * Main.rand.NextFloat(0.5f, 1.2f), ModContent.ProjectileType<BreakerBolt>(), Item.damage, Item.knockBack, player.whoAmI);
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IceBlade)
                .AddIngredient(ItemID.Frostbrand)
                .AddIngredient(ItemID.SnowBlock, 1000)
                .AddIngredient(ModContent.ItemType<Orichalcon>())
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 150)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}