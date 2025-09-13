using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Placeable;

namespace UniverseOfSwordsMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class BlueDamascusHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Blue Damascus Helmet");
			/* Tooltip.SetDefault("'Armor for durable warriors'"
			    + "\n6% increased melee speed"
			    + "\n6% increased melee critical chance"); */
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 18;
			Item.value = Item.buyPrice(gold: 7);
			Item.rare = ItemRarityID.Cyan;
			Item.defense = 30;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.06f;
			player.GetCritChance(DamageClass.Melee) += 6;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
			recipe.AddIngredient(null, "DamascusHelmet", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddIngredient(ItemID.IronskinPotion, 15);
			recipe.AddIngredient(ItemID.HallowedMask, 1);
            recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);			
			recipe.Register();
		}
	}
}