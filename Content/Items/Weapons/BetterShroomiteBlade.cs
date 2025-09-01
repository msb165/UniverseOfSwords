using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BetterShroomiteBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Bigger and better!");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;
            Item.useAnimation = 20;           
            Item.damage = 78; 
            Item.knockBack = 7.2f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 38);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }  

        public override bool? UseItem(Player player)
        {
            float spawnRot = MathHelper.PiOver4;
            if (player.direction == -1)
            {
                spawnRot += MathHelper.Pi;
            }
            Vector2 spawnVel = Vector2.Normalize((player.itemRotation + spawnRot * -player.direction).ToRotationVector2()) * Main.rand.Next(10, 31);

            if (player.itemAnimation % 3f == 0f)
            {
                Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center + spawnVel / 10, spawnVel, ProjectileID.Mushroom, Item.damage / 5, Item.knockBack, player.whoAmI);
            }
            return null;
        }
        
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShroomiteBlade>(), 1);
			recipe.AddIngredient(null, "UpgradeMatter", 1);
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
	    }
    }
}