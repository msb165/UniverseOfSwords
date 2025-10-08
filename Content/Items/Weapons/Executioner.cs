using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Executioner : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Executioner");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 78;
            Item.height = 78;			
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 15;
            Item.useAnimation = 20;           
            Item.damage = 70;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileID.CrystalLeafShot;
            Item.shootSpeed = 2f;
            Item.value = Item.sellPrice(gold: 15);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            Vector2 newSpeed = new(1f * player.direction, 1f);
            Projectile.NewProjectile(source, position + new Vector2(0f, -200f), newSpeed, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, player.MountedCenter + new Vector2(0f, -100f), newSpeed, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position + new Vector2(-player.direction * 250f, -200f), newSpeed, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position + new Vector2(player.direction * 100f, -200f), newSpeed, type, damage, knockback, player.whoAmI);

            return false;
		}
		
		public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X -= 5f * player.direction;
            player.itemLocation.Y -= 5f * player.gravDir;
        }
    }
}