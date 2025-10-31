using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class StickyGlowstickSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 46;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 15;
            Item.knockBack = 3.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 12);
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override bool? UseItem(Player player)
        {
            Lighting.AddLight(player.itemLocation, Color.SkyBlue.ToVector3());
            return null;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White with { A = 0 };

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                Lighting.AddLight(player.itemLocation, Color.SkyBlue.ToVector3());
                UniverseUtils.CustomHoldStyle(player, new Vector2(16f * player.direction, -24f), Vector2.Zero);
            }
        }
    }
}
