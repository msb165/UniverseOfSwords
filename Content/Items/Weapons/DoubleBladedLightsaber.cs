using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Held;

namespace UniverseOfSwords.Content.Items.Weapons     
{
    public class DoubleBladedLightsaber : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Double Bladed Lightsaber");
			// Tooltip.SetDefault("'Watch out to not cut your body in half'");
		}
		
        public override void SetDefaults()
        {
			Item.damage = 65;
            Item.DamageType = DamageClass.MeleeNoSpeed;   
            Item.width = 140;  
            Item.height = 140;  
			Item.scale = 1f;
            Item.useTime = 10; 
            Item.useAnimation = 10;    
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;    
            Item.knockBack = 8f; 
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Lime;                      
            Item.shoot = ModContent.ProjectileType<DoubleBladedLightsaberProjectile>();  
            Item.noUseGraphic = true; 
			Item.noMelee = true;
        }

        public override bool MeleePrefix() => true;

		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.YellowPhasesaber, 1)
                .AddIngredient(ItemID.WhitePhasesaber, 1)
                .AddIngredient(ItemID.PurplePhasesaber, 1)
                .AddIngredient(ItemID.GreenPhasesaber, 1)
                .AddIngredient(ItemID.BluePhasesaber, 1)
                .AddIngredient(ItemID.RedPhasesaber, 1)
                .AddIngredient(ItemID.SoulofFright, 12)
                .AddIngredient(ItemID.SoulofSight, 12)
                .AddIngredient(ItemID.SoulofMight, 12)
                .AddIngredient(ModContent.ItemType<HumanBuzzSaw>())
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 15)
                .AddIngredient(ItemID.CrystalShard, 50)
                .AddTile(TileID.MythrilAnvil)
                .Register();
	    } 
	}
}