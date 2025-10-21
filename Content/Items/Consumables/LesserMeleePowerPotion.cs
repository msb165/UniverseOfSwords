using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Content.Items.Materials;

namespace UniverseOfSwords.Content.Items.Consumables
{
    public class LesserMeleePowerPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;             
            Item.useStyle = ItemUseStyleID.EatFood;              
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = Item.CommonMaxStack;                
            Item.consumable = true;          
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Orange;
            Item.buffType = ModContent.BuffType<LesserMeleePower>();    
            Item.buffTime = 10000;    
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 15)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}