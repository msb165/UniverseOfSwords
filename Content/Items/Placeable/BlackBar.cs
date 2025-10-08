using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using UniverseOfSwords.Content.Tiles;

namespace UniverseOfSwords.Content.Items.Placeable
{
    public class BlackBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Damascus Bar");
            // Tooltip.SetDefault("'Material for creating powerful swords'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(24);
            Item.useTime = 20; 
            Item.useAnimation = 20; 
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(silver: 40);
            Item.rare = ItemRarityID.Green; 
            Item.UseSound = SoundID.Item1; 
            Item.autoReuse = true; 
            Item.consumable = true; 
            Item.createTile = ModContent.TileType<BlackBarTile>();
            Item.maxStack = Item.CommonMaxStack;
        }
    }
}