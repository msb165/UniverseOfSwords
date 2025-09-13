using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class NatureSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Sword made out of only pure ingredients given from Mother Nature'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 72;
            Item.height = 72; 
			Item.scale = 1.0F;
            Item.rare = 2;            
            Item.useStyle = 1;             
            Item.useTime = 25;
            Item.useAnimation = 25;           
            Item.damage = 15; 
            Item.knockBack = 6.0F;
			Item.shoot = ProjectileID.VilethornBase;
            Item.shootSpeed = 20;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(silver: 50);		
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 3, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
			}
		}
        
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Vilethorn, 1);
			recipe.AddIngredient(ItemID.Seed, 10);
			recipe.AddIngredient(ItemID.Daybloom, 5);
			recipe.AddIngredient(ItemID.DirtBlock, 100);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 40);
            recipe.AddTile(TileID.Anvils);			
            recipe.Register();
			
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TheRottedFork, 1);
			recipe.AddIngredient(ItemID.Seed, 10);
			recipe.AddIngredient(ItemID.Daybloom, 5);
			recipe.AddIngredient(ItemID.DirtBlock, 100);
			recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 40);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
	    }
    }
}
