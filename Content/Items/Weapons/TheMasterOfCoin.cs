using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TheMasterOfCoin : ModItem
    {
		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("The Master of Coin");
			// Tooltip.SetDefault("'End your financial problems with this sword'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 48;
            Item.height = 48;		
			Item.scale = 1.2F;
            Item.rare = 11;            
            Item.useStyle = 1;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.UseSound = SoundID.Item43;
			Item.shoot = ProjectileID.CoinPortal ;
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(copper: 1);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
	   
       	public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}
		
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 5f * 0.0174f; //Replace 45 with whatever spread you want
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y)- spread/2;
            double deltaAngle = spread/1f;
            double offsetAngle;
            int i;
            for (i = 0; i < 3;i++ ) //Replace 2 with number of projectiles
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }
        
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LuckyCoin, 1);
			recipe.AddIngredient(ItemID.GoldCrown, 10);
			recipe.AddIngredient(ItemID.GoldOre, 999);
			recipe.AddIngredient(ItemID.FlaskofGold, 60);
			recipe.AddIngredient(null, "Inflation", 1);
			recipe.AddIngredient(ItemID.PlatinumCoin, 9);
			recipe.AddIngredient(ItemID.GoldCoin, 99);
			recipe.AddIngredient(ItemID.SilverCoin, 999);
			recipe.AddIngredient(ItemID.CopperCoin, 999);
            recipe.AddTile(TileID.LunarCraftingStation);			
            recipe.Register();
			
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LuckyCoin, 1);
			recipe.AddIngredient(ItemID.PlatinumCrown, 10);
			recipe.AddIngredient(ItemID.PlatinumOre, 999);
			recipe.AddIngredient(ItemID.FlaskofGold, 60);
			recipe.AddIngredient(null, "Inflation", 1);
			recipe.AddIngredient(ItemID.PlatinumCoin, 9);
			recipe.AddIngredient(ItemID.GoldCoin, 99);
			recipe.AddIngredient(ItemID.SilverCoin, 999);
			recipe.AddIngredient(ItemID.CopperCoin, 999);
            recipe.AddTile(TileID.LunarCraftingStation);			
            recipe.Register();
	    } 
    }
}