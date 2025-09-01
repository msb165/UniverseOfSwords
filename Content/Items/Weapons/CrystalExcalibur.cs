using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class CrystalExcalibur : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Sword forged in heaven'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 84;
            Item.height = 84; 
			Item.scale = 1.5f;
            Item.rare = ItemRarityID.Yellow;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 7;
            Item.useAnimation = 7;           
            Item.damage = 150; 
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item1;
            Item.value = 990000;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.MeleeNoSpeed;
	    }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				UniverseUtils.SpawnRotatedDust(player, DustID.IceTorch, 2f, (int)(90 * Item.scale), (int)(126 * Item.scale));
				UniverseUtils.SpawnRotatedDust(player, DustID.PinkTorch, 2f);
			}
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "OmegaExcalibur", 1);
			recipe.AddIngredient(null, "AlphaExcalibur", 1);
			recipe.AddIngredient(null, "AncientKatana", 1);
			recipe.AddIngredient(ItemID.CrystalShard, 20);
			recipe.AddIngredient(ItemID.PixieDust, 15);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe.AddIngredient(ItemID.DarkShard, 1);
			recipe.AddIngredient(ItemID.LightShard, 1);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
			recipe.AddIngredient(null, "MartianSaucerCore", 1);
			recipe.AddIngredient(null, "Orichalcon", 1);
			recipe.AddIngredient(null, "SwordMatter", 150);
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
	    }
    }
}