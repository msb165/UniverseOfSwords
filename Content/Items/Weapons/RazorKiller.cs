using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
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
            for (int i = 0; i < 3; i++) //Replace 2 with number of projectiles
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center, newVel, ProjectileID.PineNeedleFriendly, Item.damage, hit.Knockback, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Razorpine, 1);
            recipe.AddIngredient(null, "Orichalcon", 1);
            recipe.AddIngredient(null, "SwordMatter", 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}