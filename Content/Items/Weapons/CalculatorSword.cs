using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class CalculatorSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Sprite was made in the calculator. True story.");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 48;
            Item.height = 52; 
			Item.scale = 1f;
            Item.rare = ItemRarityID.Green;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 25;
            Item.useAnimation = 25;           
            Item.damage = 18; 
            Item.knockBack = 5.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(silver: 20);		
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(4))
			{
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.WhiteTorch, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
			}
		}
        
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup("IronBar", 5);
			recipe.AddIngredient(ItemID.CopperBar, 10);
            recipe.AddIngredient(null, "SwordMatter", 20);
            recipe.AddTile(TileID.Anvils);			
            recipe.Register();
			
			recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 5);
			recipe.AddIngredient(ItemID.TinBar, 10);
			recipe.AddIngredient(null, "SwordMatter", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
	    }
    }
}
