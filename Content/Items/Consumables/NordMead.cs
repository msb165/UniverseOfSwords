using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;

namespace UniverseOfSwordsMod.Content.Items.Consumables
{
    public class NordMead : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nord Mead");
        }

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;               
            Item.useStyle = ItemUseStyleID.DrinkLong;       
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = Item.CommonMaxStack;            
            Item.width = 20;
            Item.height = 36;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.Orange;
            Item.buffType = ModContent.BuffType<Buffs.NordMead>();    
            Item.buffTime = 14000;
            Item.consumable = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Seed, 10)
                .AddIngredient(ItemID.BottledHoney, 1)
                .AddTile(TileID.Kegs)
                .Register();
        }
    }
}