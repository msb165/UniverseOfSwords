using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class DranHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Maw of the Monster King");
			/* Tooltip.SetDefault("'These jaws know only hunger for the next soul'"
			+ "\n50% increased critical chance"
			+ "\nMaximum life increased by 1000"
			+ "\n50% increased damage"
			+ "\nReduces mana usage by 90%"
			+ "\nReduces ammo usage by 20%"
			+ "\nReduces throwing usage by 50%"
			+ "\nIncreased mana regeneration"
            + "\nGrants Night Owl and Gills buffs"); */			
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 34;
			Item.value = Item.buyPrice(platinum: 3);
			Item.expert = true;
			Item.defense = 150;
		}
		
		public override void AddRecipes()
		{
            Mod thorium = UniverseOfSwords.Instance.ThoriumMod;
            Mod calamity = UniverseOfSwords.Instance.CalamityMod;

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddIngredient(ItemID.BossMaskBetsy, 1);
            recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 15);
            recipe.AddIngredient(null, "HaloOfHorrors", 1);
			recipe.AddIngredient(null, "SwordShard", 5);
			recipe.AddIngredient(null, "BlueDamascusHelmet", 1);
			recipe.AddIngredient(null, "GreenDamascusHelmet", 1);
			recipe.AddIngredient(null, "RedDamascusHelmet", 1);
            if (thorium is not null)
            {
                recipe.AddIngredient(thorium.Find<ModItem>("InfernoEssence"), 5);
                recipe.AddIngredient(thorium.Find<ModItem>("DeathEssence"), 5);
                recipe.AddIngredient(thorium.Find<ModItem>("OceanEssence"), 5);
            }
            if (calamity is not null)
            {
                recipe.AddIngredient(calamity.Find<ModItem>("ExoPrism"), 5);
                recipe.AddIngredient(calamity.Find<ModItem>("AshesofAnnihilation"), 5);
            }
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
		
		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Melee) += 50;
			player.GetCritChance(DamageClass.Magic) += 50;
			player.GetCritChance(DamageClass.Ranged) += 50;
			player.GetCritChance(DamageClass.Throwing) += 50;
			player.statLifeMax2 += 1000;
			player.GetDamage(DamageClass.Melee) += 0.5f;
	        player.GetDamage(DamageClass.Magic) += 0.5f;
		    player.GetDamage(DamageClass.Throwing) += 0.5f;
			player.GetDamage(DamageClass.Summon) += 0.5f;
			player.GetDamage(DamageClass.Ranged) += 0.5f;
			player.manaRegen += 30;
			player.AddBuff(BuffID.NightOwl, 2);
			player.AddBuff(BuffID.Gills, 2);
			player.manaCost -= 0.9f;
			player.ThrownCost50 = true;
            player.ammoCost80 = true;
		}
	}
}