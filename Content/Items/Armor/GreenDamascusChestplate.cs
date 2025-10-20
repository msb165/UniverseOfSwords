using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class GreenDamascusChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Green Damascus Chestplate");
			/* Tooltip.SetDefault("'Armor for fast warriors'"
			    + "\n15% increased melee damage"
				+ "\n10% increased melee speed"); */
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.buyPrice(gold: 7);
			Item.rare = ItemRarityID.Green;
			Item.defense = 30;
		}
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Mod.Find<ModItem>("GreenDamascusHelmet").Type && legs.type == Mod.Find<ModItem>("GreenDamascusLeggings").Type;
        }
		
		public override void UpdateArmorSet(Player player)
		{
			//player.setBonus = "15% endurance, 25% increased melee speed, 7% increased melee critical chance, 50% increased movement speed, increases maximum life by 20";
			player.setBonus = (string)Language.GetOrRegister("Mods.UniverseOfSwords.GreenDamascusArmor.SetBonus");
			player.GetAttackSpeed(DamageClass.Melee) += 0.25f;
			player.GetCritChance(DamageClass.Melee) += 7;
			player.moveSpeed += 0.40f;
			player.statLifeMax2 += 20;
            player.endurance += 0.15f;
        }

        public override void UpdateEquip(Player player)
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
			player.GetDamage(DamageClass.Melee) += 0.15f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
			recipe.AddIngredient(null, "DamascusBreastplate", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddIngredient(ItemID.SwiftnessPotion, 15);
			recipe.AddIngredient(ItemID.BeetleScaleMail, 1);
			recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddTile(TileID.MythrilAnvil);			
			recipe.Register();
		}
	}
}