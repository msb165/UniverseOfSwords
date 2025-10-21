using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DraculaSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.75f;
        }
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 68;
            Item.knockBack = 6f;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(32f * player.direction, -24f), new Vector2(0f, 4f));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            target.AddBuff(BuffID.Bleeding, 360);
            for (int i = 0; i < 3; i++)
            {
                Vector2 spawnVel = (Vector2.UnitY * 16f).RotatedBy(i * MathHelper.TwoPi / 3f);
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - spawnVel * 10f, spawnVel, ModContent.ProjectileType<DraculaProj>(), Item.damage, 4f, player.whoAmI, ai1: target.whoAmI, ai2: 1f);
            }
        }
    }
}