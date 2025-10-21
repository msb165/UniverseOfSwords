using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TheBrain : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Sword of Crimson'");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 24;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 50);
            Item.shoot = ModContent.ProjectileType<DraculaProj>();
            Item.shootSpeed = 3f;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.CrimsonSpray, 0.95f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 newVel = Vector2.Normalize(velocity) * 3.5f;
            Projectile.NewProjectile(source, position + newVel, newVel.RotatedByRandom(MathHelper.ToRadians(2.5f)), type, damage, knockback / 2, player.whoAmI);
            return false;
        }
    }
}
