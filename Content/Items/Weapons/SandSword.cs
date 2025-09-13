using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SandSword : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 36;
            Item.height = 36; 
			Item.scale = 1.0F;
            Item.rare = ItemRarityID.White;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 7; 
            Item.knockBack = 2.4F;
            Item.UseSound = SoundID.Item1;
            Item.value = 150;			
            Item.autoReuse = false; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Gold, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
			}
		}
        
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SandBlock, 15);
            recipe.AddTile(TileID.WorkBenches);			
            recipe.Register();
	    }
    }
}