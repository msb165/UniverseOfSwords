using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Materials
{
	public class MartianSaucerCore : ModItem
	{
		public override void SetStaticDefaults()
		{
            // Tooltip.SetDefault("Pulses with space energy");
        }

        public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 40;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}