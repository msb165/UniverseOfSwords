using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class IceBreaker : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Freezing to the touch'");
		}
		
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 40;
            Item.useAnimation = 20;           
            Item.damage = 61; 
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item28;
			Item.shoot = ProjectileID.FrostBoltSword;
            Item.shootSpeed = 20f;
            Item.value = 300200;			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
	    } 

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                Dust dust = Dust.NewDustDirect(player.itemLocation - new Vector2(-32f * player.direction, 80f), 64, 64, DustID.SpectreStaff, 0, 0, 127, default, 1f);
                if (player.direction == -1)
                {
                    dust.position.X -= 48f;
                }
                dust.noGravity = true;
                dust.velocity.X = Main.rand.Next(-10, 11) * 0.5f * player.direction;
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), new Vector2(0f, 4f));
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 2 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(10f);
			position += Vector2.Normalize(velocity) * 5f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IceBlade)
                .AddIngredient(ItemID.Frostbrand)
                .AddIngredient(ItemID.SnowBlock, 1000)
                .AddIngredient(ModContent.ItemType<Orichalcon>())
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 150)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}