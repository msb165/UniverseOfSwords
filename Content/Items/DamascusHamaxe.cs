using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items
{
	public class DamascusHamaxe : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 38;
			Item.height = 38;
			Item.useTime = 19;
			Item.useAnimation = 17;
			Item.axe = 10;
			Item.hammer = 50;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(silver: 20);
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DamascusBar", 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}