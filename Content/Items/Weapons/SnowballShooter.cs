using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SnowballShooter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 45;
            Item.useAnimation = 15;
            Item.damage = 67;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.RocketSnowmanI;
            Item.shootSpeed = 6f;
            Item.value = Item.sellPrice(silver: 80);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.scale = 1.25f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 4; i++)
            {
                Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(45f)) * Main.rand.NextFloat(1f, 1.25f), ModContent.ProjectileType<Snowball>(), damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center, (target.Center - player.Center).RotatedByRandom(MathHelper.ToRadians(15f)) * 0.125f, ModContent.ProjectileType<Rocket>(), damageDone, hit.Knockback, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SnowmanCannon)
                .AddIngredient(ModContent.ItemType<Orichalcon>())
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 150)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}