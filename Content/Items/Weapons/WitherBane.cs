using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
	public class WitherBane : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wither Bane");
		}
		
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 42;
			Item.scale = 1.1F;
			Item.rare = ItemRarityID.Green;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.damage = 19;
			Item.knockBack = 3.0F;
			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(gold: 1);
			Item.autoReuse = true;
			Item.DamageType = DamageClass.Melee;
		}
		
        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X += 5f * player.direction;
            player.itemLocation.Y += 5f * player.gravDir;
        }
	}
}