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
    public class SkyPower : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 28;
            Item.useAnimation = 17;
            Item.damage = 60;
            Item.knockBack = 7f;
            Item.shoot = ModContent.ProjectileType<SkyProj>();
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 40);
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spin = MathHelper.ToRadians(6f);
            for (int i = 0; i < 3; i++)
            {
                float offset = i - (3f - 1f) / 2f;
                Vector2 newVel = new(11f * player.direction, 24f);
                Projectile.NewProjectileDirect(source, player.Center - Vector2.UnitY * player.height * 4f + newVel, newVel.RotatedBy(spin * offset) * Main.rand.NextFloat(0.5f, 1.25f), type, damage / 3, knockback, player.whoAmI, ai1: Main.rand.Next(14));
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}