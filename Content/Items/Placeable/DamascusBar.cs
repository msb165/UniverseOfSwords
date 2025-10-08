using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace UniverseOfSwords.Content.Items.Placeable
{
    public class DamascusBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Damascus Bar");
            // Tooltip.SetDefault("'Material for creating powerful swords'");
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 24; // Hitbox Width
            Item.height = 24; // Hitbox Height
            Item.useTime = 20; // Speed before reuse
            Item.useAnimation = 20; // Animation Speed
            Item.useStyle = ItemUseStyleID.Swing; // 1 = Broadsword 
            Item.value = 2000; // 10 | 00 | 00 | 00 : Platinum | Gold | Silver | Bronze
            Item.rare = ItemRarityID.Green; // Item Tier
            Item.UseSound = SoundID.Item1; // Sound effect of item on use 
            Item.autoReuse = true; // Do you want to torture people with clicking? Set to false
            Item.consumable = true; // Will consume the item when placed.
            Item.createTile = Mod.Find<ModTile>("DamascusBarTile").Type;
            Item.maxStack = Item.CommonMaxStack; // The maximum number you can have of this item.
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "DamascusOre", 4);
            r.AddTile(TileID.Furnaces);
            r.Register();
        }
    }
}