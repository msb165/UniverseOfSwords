using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class MagnetSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots Magnet spheres");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 74;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item43;
            Item.value = Item.sellPrice(silver: 35);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            for (int i = 0; i < 2; i++)
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.MagnetSphere, 2f, (int)(34 * Item.scale), (int)(84 * Item.scale));
            }
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                Vector2 spawnPos = Main.rand.NextVector2Circular(200f, 200f);
                Vector2 spawnVel = spawnPos.SafeNormalize(Vector2.UnitY);
                spawnPos = player.Center - spawnPos;

                if ((Collision.SolidTiles(spawnPos, 32, 32)))
                {
                    continue;
                }
                Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, spawnVel, ModContent.ProjectileType<MagnetSphereBall>(), hit.Damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}