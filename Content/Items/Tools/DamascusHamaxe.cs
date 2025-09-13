using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Placeable;

namespace UniverseOfSwordsMod.Content.Items.Tools
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
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DamascusBar>(), 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}