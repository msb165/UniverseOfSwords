using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TheStinger : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Shoots deadly Stingers");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 62;
            Item.height = 62;			
			Item.scale = 1.0F;
            Item.rare = 3;            
            Item.useStyle = 1;             
            Item.useTime = 20;
            Item.useAnimation = 20;           
            Item.damage = 23;
            Item.knockBack = 5.0F;
			Item.shoot = ProjectileID.HornetStinger ;
            Item.shootSpeed = 10;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 50);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "SwordMatter", 100);
			recipe.AddIngredient(ItemID.Vine, 1);
			recipe.AddIngredient(ItemID.Stinger, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
	    } 
	   
	    public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(4) == 0)
			{

                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 128, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
			}
		}
    }
}