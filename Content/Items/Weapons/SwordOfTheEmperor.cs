using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SwordOfTheEmperor : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of The Emperor");
            // Tooltip.SetDefault("'Grant them the Emperor's mercy. Let the heretics burn!'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(51);
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.damage = 100;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item74;
            Item.value = 0;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.InfernoFork, 1.5f, 32, 200);
            }
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<EmperorBlaze>(), 100);
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(BuffID.Midas, 360);
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(ModContent.BuffType<TrueSlow>(), 60);
            }
            if (UniverseUtils.IsAValidTarget(target))
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ProjectileID.DaybreakExplosion, Item.damage, 10f, player.whoAmI, ai1: 0.85f + Main.rand.NextFloat() * 1.15f);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 200)
                .AddIngredient(ItemID.HallowedBar, 4000)
                .AddIngredient(ItemID.BrokenHeroSword, 16)
                .AddIngredient(ItemID.EnchantedSword)
                .AddIngredient(ItemID.Arkhalis)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 200)
                .AddIngredient(ItemID.HallowedBar, 4000)
                .AddIngredient(ItemID.BrokenHeroSword, 16)
                .AddIngredient(ItemID.EnchantedSword)
                .AddIngredient(ItemID.Terragrim)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}