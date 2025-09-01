using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
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
            Item.width = 132;
            Item.height = 132;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 88;
            Item.knockBack = 3f;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwordsMod)}/Assets/Sounds/Item/PianoYellow");
            Item.shoot = ProjectileID.HolyArrow;
            Item.shootSpeed = 5f;
            Item.value = Item.sellPrice(gold: 12);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X -= 1f * player.direction;
            player.itemLocation.Y -= 1f * player.direction;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PearlwoodPiano, 1);
            recipe.AddIngredient(ItemID.MartianPiano, 1);
            recipe.AddIngredient(ItemID.CrystalPiano, 1);
            recipe.AddIngredient(ItemID.SpookyPiano, 1);
            recipe.AddIngredient(ItemID.FleshPiano, 1);
            recipe.AddIngredient(ItemID.LihzahrdPiano, 1);
            recipe.AddIngredient(ItemID.SteampunkPiano, 1);
            recipe.AddIngredient(ItemID.GoldenPiano, 1);
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