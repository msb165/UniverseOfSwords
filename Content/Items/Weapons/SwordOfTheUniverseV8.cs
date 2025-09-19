using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SwordOfTheUniverseV8 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Universe");
            /* Tooltip.SetDefault("'This sword doesn't swing. It lifts the Universe towards the blade'"
			    + "\nHas changeable forms"); */
        }

        public override void SetDefaults()
        {
            Item.width = 140;
            Item.height = 140;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Purple;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 275;
            Item.knockBack = 20f;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwordsMod)}/Assets/Sounds/Item/GiantExplosion");
            Item.expert = true;
            Item.value = Item.sellPrice(platinum: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV2>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV3>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV4>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV5>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV6>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV7>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV8>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV9>())
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverse>())
                .Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PinkTorch, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X -= player.direction * 0f;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.Ichor, 360);
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.OnFire, 360);
            target.AddBuff(BuffID.Poisoned, 360);
            target.AddBuff(BuffID.CursedInferno, 360);
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 360);
            float piOverTen = MathHelper.Pi / 10f;
            Vector2 baseVel = (target.Center - player.Center).SafeNormalize(Vector2.Zero) * 40f;
            Vector2 newVel = (target.Center - player.Center).SafeNormalize(Vector2.Zero) * 15f;
            for (int i = 0; i < 5; i++)
            {
                float offset = i - (5f - 1f) / 2f;
                Vector2 velOffset = baseVel.RotatedBy(piOverTen * offset);
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), player.RotatedRelativePoint(player.MountedCenter) + velOffset, newVel, ModContent.ProjectileType<SOTU8>(), Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}