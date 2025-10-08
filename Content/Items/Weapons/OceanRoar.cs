using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class OceanRoar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 50;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.damage = 10;
            Item.shoot = ModContent.ProjectileType<Twirl>();
            Item.shootSpeed = 2f;
            Item.UseSound = SoundID.Item84;
            Item.value = Item.sellPrice(silver: 25);
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // ai[0] = rotation
            // ai[1] = velocity

            Projectile.NewProjectile(source, position + velocity * 12f, velocity, type, damage, knockback, player.whoAmI, ai0: Utils.SelectRandom(Main.rand, [-0.125f, -0.1f, 0.1f, 0.125f]), ai1: Main.rand.NextFloat(0.93f, 0.97f));
            return false;
        }
    }
}