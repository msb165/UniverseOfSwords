using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class FrozenShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Inflicts Frostburn debuff");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 56;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 66;
            Item.knockBack = 8.0F;
            Item.shoot = ModContent.ProjectileType<Frozen>();
            Item.shootSpeed = 4f;
            Item.UseSound = SoundID.Item28;
            Item.value = Item.sellPrice(gold: 15);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.IceTorch, 1.5f);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 500);
            target.AddBuff(BuffID.Frostburn2, 500);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += velocity * 8f - Vector2.UnitY * 24f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VenomShard>());
            recipe.AddIngredient(ItemID.FrostCore, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}