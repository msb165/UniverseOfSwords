using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BeliarClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Claw of Beliar");
            // Tooltip.SetDefault("Pulses with dark energy of Beliar");
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 100;
            Item.knockBack = 12f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }
        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center + new Vector2(0f, 4f);

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(56f * player.direction, -64f), new Vector2(0f, 4f));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - Vector2.UnitY * 72, Vector2.Zero, ModContent.ProjectileType<BeliarLightning>(), hit.Damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}