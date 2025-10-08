using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PianoSword4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Liszt Lasher");
            // Tooltip.SetDefault("'Piano sonata in B minor - Liszt'");
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 66;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 88;
            Item.knockBack = 3f;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwords)}/Assets/Sounds/Item/PianoYellow") with { PitchVariance = 0.5f };
            Item.shoot = ProjectileID.HolyArrow;
            Item.shootSpeed = 3f;
            Item.value = Item.sellPrice(gold: 12);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PearlwoodPiano);
            recipe.AddIngredient(ItemID.MartianPiano);
            recipe.AddIngredient(ItemID.CrystalPiano);
            recipe.AddIngredient(ItemID.SpookyPiano);
            recipe.AddIngredient(ItemID.FleshPiano);
            recipe.AddIngredient(ItemID.LihzahrdPiano);
            recipe.AddIngredient(ItemID.SteampunkPiano);
            recipe.AddIngredient(ItemID.GoldenPiano);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.GoblinSorcerer, 1.5f, alpha: 0);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < Main.rand.Next(1, 4); i++)
            {
                Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.75f, 1.25f);
                Projectile.NewProjectile(source, position + newVel * 8f, newVel, ModContent.ProjectileType<Note>(), damage / 2, knockback, player.whoAmI, ai1: Main.rand.Next(0, 3), ai2: Main.rand.NextFloat(0.1f, 0.4f));
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 360);
            target.AddBuff(BuffID.Bleeding, 360);
            target.AddBuff(BuffID.Midas, 360);
        }
    }
}