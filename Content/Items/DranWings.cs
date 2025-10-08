using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Placeable;

namespace UniverseOfSwords.Content.Items
{
	[AutoloadEquip(EquipType.Wings)]
	public class DranWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("10 seconds fly time"
			    + "\nHigh acceleration"
				+ "\nGreat speed"
				+ "\nAllows ability to dash"
				+ "\n20% increased movement speed"
				+ "\nIncreases maximum life by 250"
			    + "\n20% increased damage"
				+ "\nGrants gravitation effect"); */
		}

		public override void SetDefaults()
		{
			Item.width = 78;
			Item.height = 54;
			Item.value = Item.sellPrice(platinum: 1);
			Item.rare = ItemRarityID.Red;
			Item.accessory = true;
			Item.defense = 50;
		}
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 550;
			player.dash = 1;
			player.statLifeMax2 += 250;
			player.moveSpeed += 0.20f;
            player.GetDamage(DamageClass.Melee) += 0.20f;
	        player.GetDamage(DamageClass.Magic) += 0.20f;
		    player.GetDamage(DamageClass.Throwing) += 0.20f;
			player.GetDamage(DamageClass.Summon) += 0.20f;
			player.GetDamage(DamageClass.Ranged) += 0.20f;
			player.AddBuff(BuffID.Gravitation, 2);
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.30f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 15f;
			acceleration *= 10.0f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DD2PetDragon, 1);
			recipe.AddIngredient(ItemID.SoulofFlight, 40);
			recipe.AddIngredient(ModContent.ItemType<BlackBar>(), 15);
			recipe.AddIngredient(null, "LunarOrb", 6);
			recipe.AddIngredient(null, "HaloOfHorrors", 1);
			recipe.AddIngredient(ItemID.WingsVortex, 1);
			recipe.AddIngredient(ItemID.WingsNebula, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}