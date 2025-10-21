using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Golem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Ancient sword that was inside Golems body");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.damage = 95;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 4);
            Item.shoot = ModContent.ProjectileType<GolemLaser>();
            Item.shootSpeed = 10f;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(64f * player.direction, -54f), Vector2.UnitY * 4f);
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.Lihzahrd, 1f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) => false;

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            target.AddBuff(BuffID.Oiled, 300);
            /*            for (int j = 0; j < 3; j++)
                        {
                            Vector2 spawnPos = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitY) * 40f;
                            float offset = j * (3f - 1f) / 2f;
                            Vector2 spawnOffset = spawnPos.RotatedBy(offset * MathHelper.TwoPi / 3f);
                            Vector2 spawnVel = Vector2.Normalize(Main.MouseWorld - player.Center - spawnOffset);
                            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + spawnOffset - Vector2.UnitY * 12f, spawnVel * Item.shootSpeed, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
                        }*/

            for (int j = 0; j < 4; j++)
            {
                Vector2 spawnPos = player.Center - Vector2.UnitY * 12f;
                Vector2 newVel = (target.Center - spawnPos).SafeNormalize(Vector2.UnitY) * Item.shootSpeed;
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center - Vector2.UnitY * 12f, newVel.RotatedByRandom(MathHelper.ToRadians(25f)), Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}
