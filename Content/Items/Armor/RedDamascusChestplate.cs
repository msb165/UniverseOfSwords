using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Placeable;

namespace UniverseOfSwordsMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class RedDamascusChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Red Damascus Chestplate");
			/* Tooltip.SetDefault("'Armor for agressive warriors'"
			    + "\n10% increased melee damage"
				+ "\n10% increased melee critical chance"); */
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.buyPrice(gold: 7);
			Item.rare = 3;
			Item.defense = 20;
		}
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Mod.Find<ModItem>("RedDamascusHelmet").Type && legs.type == Mod.Find<ModItem>("RedDamascusLeggings").Type;
        }
		
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Grants Titan potion effect, 10% increased melee damage, 10% increased melee critical chance, increases maximum life by 20";
			player.GetDamage(DamageClass.Melee) += 0.10f;
		    player.AddBuff(BuffID.Titan, 2);
			player.GetCritChance(DamageClass.Melee) += 10;
			player.statLifeMax2 += 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.10f;
			player.GetCritChance(DamageClass.Melee) += 10;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 15);
			recipe.AddIngredient(null, "DamascusBreastplate", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddIngredient(ItemID.WrathPotion, 15);
			recipe.AddIngredient(ItemID.HallowedPlateMail, 1);
			recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddTile(TileID.MythrilAnvil);			
			recipe.Register();
		}
	}
}