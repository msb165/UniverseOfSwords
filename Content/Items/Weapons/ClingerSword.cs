using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Dusts;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class ClingerSword : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 48;
            Item.height = 58; 
			Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightPurple;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 20;           
            Item.damage = 50; 
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item100;
			Item.shoot = ModContent.ProjectileType<Projectiles.Common.ClingerWall>();
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(gold: 10);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                for (int i = 0; i < 3; i++)
                {
                    UniverseUtils.SpawnRotatedDust(player, ModContent.DustType<Dusts.ClingerWall>());
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center - Vector2.UnitY * 78, Vector2.UnitX * Item.shootSpeed * player.direction, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}