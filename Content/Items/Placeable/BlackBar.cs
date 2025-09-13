using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using UniverseOfSwordsMod.Content.Tiles;

namespace UniverseOfSwordsMod.Content.Items.Placeable
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
            Item.width = 24; 
            Item.height = 24;
            Item.useTime = 20; 
            Item.useAnimation = 20; 
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(silver: 40); // 10 | 00 | 00 | 00 : Platinum | Gold | Silver | Bronze
            Item.rare = ItemRarityID.Green; // Item Tier
            Item.UseSound = SoundID.Item1; // Sound effect of item on use 
            Item.autoReuse = true; // Do you want to torture people with clicking? Set to false
            Item.consumable = true; // Will consume the item when placed.
            Item.createTile = ModContent.TileType<BlackBarTile>();
            Item.maxStack = Item.CommonMaxStack;
        }
    }
}