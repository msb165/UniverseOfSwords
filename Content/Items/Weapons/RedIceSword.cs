using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class RedIceSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.3f;
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 14;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 4, copper: 5);
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.RedTorch, 1.25f, start: (int)(14 * Item.scale), end: (int)(84 * Item.scale), alpha: 80);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RedIceBlock, 25)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}