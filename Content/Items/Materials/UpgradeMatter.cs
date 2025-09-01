using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Materials
{
    public class UpgradeMatter : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Upgrade Matter");
            // Tooltip.SetDefault("'Source for upgrading swords'");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            ItemID.Sets.ItemIconPulse[Type] = true;
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.Size = new(32);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.LightRed;
        }

        public override Color? GetAlpha(Color lightColor) => lightColor with { A = 127 };
    }
}