using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Materials
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
            Item.Size = new(30);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Red;
        }
    }
}