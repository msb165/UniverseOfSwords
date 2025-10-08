using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class DranLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Overalls of the Demon Overlord");
			/* Tooltip.SetDefault("'These feet travel down the path of absolute carnage'"
			+ "\nMaximum life increased by 1000"
			+ "\n50% increased damage"
			+ "\n100% increased movement speed"
			+ "\nImmunity to knockabck"
			+ "\nGrants water walking"
			+ "\nArmor piercing increased to 300"
			+ "\nGrants immunity to most debuffs"
			+ "\nImmunity to lava"
            + "\nGrants Night Owl and Gills buffs"); */
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.buyPrice(platinum: 3);
			Item.expert = true;
			Item.defense = 150;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 15);
            recipe.AddIngredient(null, "HaloOfHorrors", 1);
			recipe.AddIngredient(null, "BlueDamascusLeggings", 1);
			recipe.AddIngredient(null, "GreenDamascusLeggings", 1);
			recipe.AddIngredient(null, "RedDamascusLeggings", 1);
			recipe.AddIngredient(ItemID.LavaWaders, 1);
			recipe.AddIngredient(ItemID.FrostsparkBoots, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
		
		public override void UpdateEquip(Player player)
        {
			player.statLifeMax2 += 1000;
			player.GetDamage(DamageClass.Melee) += 0.5f;
	        player.GetDamage(DamageClass.Magic) += 0.5f;
			player.GetDamage(DamageClass.Summon) += 0.5f;
			player.GetDamage(DamageClass.Ranged) += 0.5f;
			player.moveSpeed += 1f;
			player.jumpBoost = true;
			player.noFallDmg = true;
			player.longInvince = true;
			player.noKnockback = true;
			player.buffImmune[BuffID.OnFire] = true;
			player.buffImmune[BuffID.Burning] = true;
			player.lavaImmune = true;
			player.AddBuff(BuffID.WaterWalking, 18000);
        }
	}
}