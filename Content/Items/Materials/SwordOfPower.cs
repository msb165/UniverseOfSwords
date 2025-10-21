using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Materials
{
    public class SwordOfPower : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("You need to fix this sword if you want to use it");
        }
        
        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.rare = ItemRarityID.Orange;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;   
            Item.useAnimation = 20; 			
            Item.damage = 0; 
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = 0;				
            Item.autoReuse = false; 
            Item.noMelee = true;
        }   
    }
}
