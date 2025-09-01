using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Consumables
{
    public class LesserMeleePowerPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases melee stats by small amount");
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
            Item.useStyle = ItemUseStyleID.EatFood;                 //this is how the item is holded when used
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = Item.CommonMaxStack;                 //this is where you set the max stack of item
            Item.consumable = true;           //this make that the item is consumable when used
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Orange;
            Item.buffType = ModContent.BuffType<LesserMeleePower>();    //this is where you put your Buff name
            Item.buffTime = 10000;    //this is the buff duration        20000 = 6 min
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}