using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Placeable;

namespace UniverseOfSwordsMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class BlueDamascusChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Blue Damascus Chestplate");
			/* Tooltip.SetDefault("'Armor for durable warriors'"
			    + "\n7% increased melee damage"
				+ "\n10% increased movement speed"); */
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.buyPrice(gold: 7);
			Item.rare = 9;
			Item.defense = 20;
		}
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Mod.Find<ModItem>("BlueDamascusHelmet").Type && legs.type == Mod.Find<ModItem>("BlueDamascusLeggings").Type;
        }
		
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "10% endurance, 7% increased melee critical chance, increases maximum life by 60";
			player.endurance += 0.10f;
			player.GetCritChance(DamageClass.Melee) += 7;
			player.statLifeMax2 += 60;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.07f;
			player.moveSpeed += 0.10f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
			recipe.AddIngredient(null, "DamascusBreastplate", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddIngredient(ItemID.IronskinPotion, 15);
			recipe.AddIngredient(ItemID.HallowedPlateMail, 1);
            recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);			
			recipe.Register();
		}
	}
}