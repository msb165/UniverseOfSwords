using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class DirtSword : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 36;
            Item.height = 36; 
			Item.scale = 1f;
            Item.rare = ItemRarityID.White;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 6; 
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = 15;			
            Item.autoReuse = false; 
            Item.DamageType = DamageClass.Melee;
	    }
	           
		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 25)
                .AddTile(TileID.WorkBenches)
                .Register();
	    }
    }
}
