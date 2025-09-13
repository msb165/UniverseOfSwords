using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class DamascusBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Damascus Breastplate");
			// Tooltip.SetDefault("4% increased melee damage");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.buyPrice(gold: 2);
			Item.rare = 2;
			Item.defense = 6;
		}
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Mod.Find<ModItem>("DamascusHelmet").Type && legs.type == Mod.Find<ModItem>("DamascusLeggings").Type;
        }
		
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "4 extra defense, 4% increased melee damage, 3% increased melee speed, 4% increased melee critical chance";
			player.GetDamage(DamageClass.Melee) += 0.04f;
		    player.statDefense += 4;
			player.GetAttackSpeed(DamageClass.Melee) += 0.03f;
			player.GetCritChance(DamageClass.Melee) += 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.04f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DamascusBar", 20);
			recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 60);
            recipe.AddTile(TileID.Anvils);			
			recipe.Register();
		}
	}
}