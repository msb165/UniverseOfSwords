using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class FixedSwordOfPower : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Inflicts Midas debuff on enemies");
        }

        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.scale = 1.4f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 60;
            Item.useAnimation = 30;
            Item.damage = 35;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 50);
            Item.shoot = ModContent.ProjectileType<Bonerang>();
            Item.shootSpeed = 10f;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += velocity * 2.5f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360); // 6 second
            //if (UniverseUtils.IsAValidTarget(target))
            //{
            //    Projectile.NewProjectile(target.GetSource_OnHit(), )
            //}
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfPower>())
                .AddIngredient(ItemID.Bone, 25)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 100)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
