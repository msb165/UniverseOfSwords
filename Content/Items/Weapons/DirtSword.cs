using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DirtSword : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 24;
            Item.height = 24; 
			Item.scale = 1.125f;
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

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 25)
                .AddTile(TileID.WorkBenches)
                .Register();
	    }
    }
}
