using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class DamascusHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Damascus Helmet");
			// Tooltip.SetDefault("3% increased melee speed");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = 2;
			Item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.03f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DamascusBar", 10);
			recipe.AddIngredient(null, "SwordMatter", 65);
            recipe.AddTile(TileID.Anvils);			
			recipe.Register();
		}
	}
}