using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class OnyxSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots Onyx");
        }

        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 40;
            Item.knockBack = 7.7f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 5, silver: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 6f);
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Purple, end: (int)(84 * Item.scale));
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<FlyingOnyx>()] >= 3 || !UniverseUtils.IsAValidTarget(target))
            {
                return;
            }

            Vector2 spawnDistance = Main.rand.NextVector2Unit() * new Vector2(200f) * Main.rand.NextFloat(0.3f, 1f);
            Vector2 spawnPos = target.Center - spawnDistance;
            for (int i = 0; i < 10; i++)
            {
                spawnDistance = Main.rand.NextVector2Unit() * new Vector2(200f) * Main.rand.NextFloat(0.3f, 1f);
                spawnPos = target.Center - spawnDistance;
                if (!Collision.SolidTiles(spawnPos, 16, 16))
                {
                    break;
                }
            }

            Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, Vector2.Zero, ModContent.ProjectileType<FlyingOnyx>(), Item.damage, Item.knockBack, player.whoAmI, target.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 150)
                .AddIngredient(ItemID.OnyxBlaster)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}