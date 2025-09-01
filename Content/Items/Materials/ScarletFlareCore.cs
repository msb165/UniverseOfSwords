using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Materials
{
    public class ScarletFlareCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scarlet Flare Core");
            // Tooltip.SetDefault("'Core from depths of hell'");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 50;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = 10;
        }
    }
}