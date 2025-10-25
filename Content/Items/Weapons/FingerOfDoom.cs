using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class FingerOfDoom : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.rare = ItemRarityID.Yellow;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 13;
            Item.useAnimation = 13;           
            Item.damage = 80;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 40);			
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
    }
}