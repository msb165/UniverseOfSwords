using Terraria.ModLoader;
using Terraria.ID;

namespace UniverseOfSwordsMod.Content.Items.Placeable
{
    public class BlackOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Ore");
        }

        public override void SetDefaults()
        {
            Item.width = 16; // Hitbox Width
            Item.height = 16; // Hitbox Height
            Item.useTime = 15; // Speed before reuse
            Item.useAnimation = 15; // Animation Speed
            Item.useStyle = 1; // 1 = Broadsword 
            Item.value = 100000; // 10 | 00 | 00 | 00 : Platinum | Gold | Silver | Bronze
            Item.rare = 10; // Item Tier
            Item.autoReuse = true; // Do you want to torture people with clicking? Set to false
            Item.consumable = true; // Will consume the item when placed.
            Item.createTile = Mod.Find<ModTile>("BlackOreTile").Type;
            Item.maxStack = 99; // The maximum number you can have of this item.
        }
    }
}