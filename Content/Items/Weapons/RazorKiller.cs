using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class RazorKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots Razorpines");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.damage = 50;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 30);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitY) * 10f;
            for (int i = 0; i < 2; i++) //Replace 2 with number of projectiles
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center, newVel.RotatedByRandom(MathHelper.ToRadians(15f)), ModContent.ProjectileType<RazorpineProj>(), Item.damage, hit.Knockback, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Razorpine);
            recipe.AddIngredient(null, "Orichalcon", 1);
            recipe.AddIngredient(ModContent.ItemType<SwordMatter>(), 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}