using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Weapons;

namespace UniverseOfSwordsMod.Content.Items.Consumables
{
	public class RevenantBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.OpenableBag[Type] = true;
			// DisplayName.SetDefault("Revenant Bag");
			// Tooltip.SetDefault("'Something terrible is hidden inside this bag'");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Red;
		}

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Revenant>()));
        }

		
		public override void AddRecipes()
        {
            CreateRecipe()
				.AddIngredient(ItemID.CrimsonChest, 1)
                .AddIngredient(ItemID.CorruptionChest, 1)
                .AddIngredient(ItemID.HallowedChest, 1)
                .AddIngredient(ItemID.FrozenChest, 1)
                .AddIngredient(ItemID.JungleChest, 1)
                .AddIngredient(ItemID.BoneKey, 1)
                .AddIngredient(ItemID.Ectoplasm, 75)
                .AddTile(TileID.BoneWelder)
                .Register();
	    } 
	}
}