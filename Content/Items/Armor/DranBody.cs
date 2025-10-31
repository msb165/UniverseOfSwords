using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class DranBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Epidermis of the Death Emperor");
			/* Tooltip.SetDefault("'These hands have taken so many lives...'"
			+ "\nMaximum life increased by 1000"
			+ "\n50% increased damage"
			+ "\n100% increased melee damage"
			+ "\nIncreased minion capacity by 8"
			+ "\nIncreased sentries capacity by 6"
			+ "\nArmor piercing increased to 300"
			+ "\nGrants immunity to most debuffs"
			+ "\nImmunity to lava"
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
			recipe.AddIngredient(ItemID.LunarBar, 30);
            if (thorium is not null)
            {
                recipe.AddIngredient(thorium.Find<ModItem>("InfernoEssence"),10);
                recipe.AddIngredient(thorium.Find<ModItem>("DeathEssence"), 10);
                recipe.AddIngredient(thorium.Find<ModItem>("OceanEssence"), 10);
            }
            if (calamity is not null)
            {
                recipe.AddIngredient(calamity.Find<ModItem>("ExoPrism"), 10);
                recipe.AddIngredient(calamity.Find<ModItem>("AshesofAnnihilation"), 5);
            }
            recipe.AddIngredient(null, "LegendaryWarriorGauntlet");
			recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 15);
			recipe.AddIngredient(null, "HaloOfHorrors", 1);
			recipe.AddIngredient(null, "BlueDamascusChestplate", 1);
			recipe.AddIngredient(null, "GreenDamascusChestplate", 1);
			recipe.AddIngredient(null, "RedDamascusChestplate", 1);
			recipe.AddIngredient(ItemID.ObsidianRose, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}

        public override bool IsArmorSet(Item head, Item body, Item legs) => head.type == ModContent.ItemType<DranHelmet>() && legs.type == ModContent.ItemType<DranLeggings>();

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = (string)Language.GetOrRegister("Mods.UniverseOfSwords.DranArmor.SetBonus");
			player.endurance += 0.75f;
        }

        public override void UpdateEquip(Player player)
		{
			player.statLifeMax2 += 1000;
			player.GetDamage(DamageClass.Melee) += 1f;
	        player.GetDamage(DamageClass.Magic) += 0.5f;
		    player.GetDamage(DamageClass.Throwing) += 0.5f;
			player.GetDamage(DamageClass.Summon) += 0.5f;
			player.GetDamage(DamageClass.Ranged) += 0.5f;
			player.maxTurrets += 6;
			player.maxMinions += 8;
			player.GetArmorPenetration(DamageClass.Generic) += 300;
			player.lavaImmune = true;
			player.buffImmune[BuffID.Bleeding] = true;
		    player.buffImmune[BuffID.BrokenArmor] = true;
			player.buffImmune[BuffID.Burning] = true;
			player.buffImmune[BuffID.Darkness] = true;
			player.buffImmune[BuffID.Confused] = true;
			player.buffImmune[BuffID.Cursed] = true;
			player.buffImmune[BuffID.Poisoned] = true;
			player.buffImmune[BuffID.Silenced] = true;
			player.buffImmune[BuffID.Slow] = true;
			player.buffImmune[BuffID.Weak] = true;
			player.buffImmune[BuffID.Chilled] = true;
		}
	}
}