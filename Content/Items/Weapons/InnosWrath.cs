using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class InnosWrath : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Pulses with light energy of Innos");
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 170;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(gold: 4);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.GetGlobalItem<ReflectionChance>().reflectChance = 8;
            Item.ArmorPenetration = 10;
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

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.Venom, 360);
        }
    }
}