using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Materials
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
			Item.maxStack = 99;
			Item.value = 400000;
			Item.rare = ItemRarityID.Yellow;
		}
	}
}