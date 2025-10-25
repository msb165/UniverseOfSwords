using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PaladinSword : ModItem
    {
        int swingCounter = 0;
        public override void SetDefaults()
        {
            Item.Size = new(54);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.damage = 85;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.PaladinSword>();
            Item.shootSpeed = 1f;
            Item.value = Item.sellPrice(silver: 54);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.noMelee = true;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.noUseGraphic = player.ItemAnimationActive;
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swingCounter++;
            if (swingCounter > 2)
            {
                swingCounter = 0;
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai1: swingCounter);
            return false;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}