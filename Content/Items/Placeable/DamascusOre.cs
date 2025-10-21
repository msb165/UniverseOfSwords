using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Tiles;

namespace UniverseOfSwords.Content.Items.Placeable
{
    public class DamascusOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Damascus Ore");
        }

        public override void SetDefaults()
        {
            Item.width = 16; // Hitbox Width
            Item.height = 16; // Hitbox Height
            Item.useTime = 15; // Speed before reuse
            Item.useAnimation = 15; // Animation Speed
            Item.useStyle = ItemUseStyleID.Swing; // 1 = Broadsword 
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Green; // Item Tier
            Item.autoReuse = true; // Do you want to torture people with clicking? Set to false
            Item.consumable = true; // Will consume the item when placed.
            Item.createTile = ModContent.TileType<DamascusOreTile>();
            Item.maxStack = Item.CommonMaxStack; // The maximum number you can have of this item.
        }
    }
}