using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Extase : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;			
			Item.scale = 1f;
            Item.rare = ItemRarityID.LightRed; 			
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 25;
            Item.useAnimation = 25;           
            Item.damage = 15;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 60);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
    }
}