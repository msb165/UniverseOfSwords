using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class BlueDamascusLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Blue Damascus Leggings");
			/* Tooltip.SetDefault("'Armor for durable warriors'"
			    + "\n6% increased melee critical chance"
			    + "\n5% increased movement speed"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = Item.buyPrice(gold: 7);
			Item.rare = ItemRarityID.Cyan;
			Item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.05f;
			player.GetCritChance(DamageClass.Melee) += 5;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
			recipe.AddIngredient(null, "DamascusLeggings", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddIngredient(ItemID.IronskinPotion, 15);
			recipe.AddIngredient(ItemID.HallowedGreaves, 1);
            recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);			
			recipe.Register();
		}
	}
}