using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class CrystalVileSword : ModItem
    {
		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Crystal Vile Sword");
		}

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 46; 
			Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightPurple;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 50;
            Item.useAnimation = 50;           
            Item.damage = 64; 
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item101;
			Item.shoot = ProjectileID.CrystalVileShardShaft;
            Item.shootSpeed = 32f;
            Item.value = Item.sellPrice(gold: 10);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.MeleeNoSpeed;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            proj.DamageType = DamageClass.MeleeNoSpeed;
            return false;
        }
    }
}