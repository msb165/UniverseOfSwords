using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class FeatherDuster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;			
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Blue;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 25;
            Item.useAnimation = 25;           
            Item.damage = 18;
            Item.knockBack = 1.5f;
            Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<HarpyFeather>();
            Item.shootSpeed = 3f;
            Item.value = Item.sellPrice(silver: 14);			
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float horzSpeed = Main.rand.Next(10, 80) * 0.001f;
            float verticalSpeed = -Main.rand.Next(1, 80) * 0.001f;
            if (Main.rand.NextBool(2))
            {
                horzSpeed *= -1f;
            }
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.5f, 1.25f), type, damage / 3, knockback, player.whoAmI, ai0: horzSpeed, ai1: verticalSpeed);
            }
            return false;
        }
    }
}