using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common.GlobalItems;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class AncientKatana : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 68;
            Item.rare = ItemRarityID.LightPurple;
            Item.scale = 1.4f;		
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 9;
            Item.useAnimation = 9;           
            Item.damage = 70; 
            Item.knockBack = 5.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = 600000;
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
                UniverseUtils.SpawnRotatedDust(player, DustID.PortalBoltTrail, 1.5f, (int)(16 * Item.scale), (int)(90 * Item.scale));
			}
		}
        
		public override void AddRecipes()
	    {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "SwordMatter", 250);
			recipe.AddIngredient(null, "Orichalcon", 1);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddIngredient(ItemID.SoulofMight, 10);
		    recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 20);
			recipe.AddIngredient(null, "UpgradeMatter", 1);
			recipe.AddIngredient(ItemID.Katana, 1);
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
	    }
	}
}	
