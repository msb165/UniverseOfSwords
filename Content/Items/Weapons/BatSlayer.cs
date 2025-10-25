using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BatSlayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Inflicts Confused debuff on enemies");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 33;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 5);
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

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(3))
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 spawnPos = player.Center + Main.rand.NextVector2Circular(200f, 200f);
                    for (int j = 0; j < 10; j++)
                    {
                        if (!Collision.SolidTiles(target.Center - spawnPos, 16, 16))
                        {
                            break;
                        }
                        spawnPos = player.Center + Main.rand.NextVector2Circular(200f, 200f);
                    }
                    Projectile.NewProjectile(target.GetSource_OnHit(target), spawnPos, Vector2.Normalize(Main.rand.NextVector2Unit()) * 8f, ModContent.ProjectileType<Bat>(), Item.damage, Item.knockBack, player.whoAmI);
                }
            }
            target.AddBuff(BuffID.Confused, 360);
        }
    }
}