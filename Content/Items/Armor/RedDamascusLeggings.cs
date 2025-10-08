using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class RedDamascusLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Red Damascus Leggings");
			/* Tooltip.SetDefault("'Armor for agressive warriors'"
			    + "\n10% increased melee critical chance"
			    + "\n10% increased melee damage"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = Item.buyPrice(gold: 7);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Melee) += 10;
			player.GetDamage(DamageClass.Melee) += 0.10f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
			recipe.AddIngredient(null, "DamascusLeggings", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddIngredient(ItemID.WrathPotion, 15);
			recipe.AddIngredient(ItemID.HallowedGreaves, 1);
            recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);			
			recipe.Register();
		}
	}
}