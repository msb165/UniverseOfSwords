using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class BetterShroomiteBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Bigger and better!");
		}
		
        public override void SetDefaults()
        {
            Item.Size = new(32);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;
            Item.useAnimation = 20;           
            Item.damage = 78; 
            Item.knockBack = 7.2f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 15);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
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
            CreateRecipe()
                .AddIngredient(ItemID.ShroomiteBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
	    }
    }
}