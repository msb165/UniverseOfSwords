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
            float horzSpeed = Main.rand.Next(10, 80) * 0.001f;
            float verticalSpeed = -Main.rand.Next(1, 40) * 0.001f;
            if (Main.rand.NextBool(2))
            {
                horzSpeed *= -1f;
            }
            for (int i = 0; i < 3; i++)
            {
                Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.3f, 1.25f);
                Projectile.NewProjectile(source, position + newVel * 4f, newVel, type, damage / 3, knockback, player.whoAmI, ai0: horzSpeed, ai1: verticalSpeed);
            }
            return false;
        }
    }
}