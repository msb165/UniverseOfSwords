using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PoisonSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new(32);
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 30;
            Item.knockBack = 5.6F;
            Item.damage = 48;
            Item.shoot = ModContent.ProjectileType<PoisonFang>();
            Item.shootSpeed = 3f;
            Item.UseSound = SoundID.Item43;
            Item.value = Item.sellPrice(silver: 30);
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 6f);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float piOverTen = MathHelper.ToRadians(6f);
            for (int i = 0; i < 3; i++)
            {
                float offset = i - (3f - 1f) / 2f;
                Projectile.NewProjectileDirect(source, position + velocity, velocity.RotatedBy(piOverTen * offset), type, damage / 3, knockback, player.whoAmI);
            }
            return false;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}