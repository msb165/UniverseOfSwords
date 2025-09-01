using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SwordOfTheUniverseV5 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Universe");
            /* Tooltip.SetDefault("'This sword doesn't swing. It lifts the Universe towards the blade'"
			    + "\nHas changeable forms"); */
        }

        public override void SetDefaults()
        {
            Item.width = 166;
            Item.height = 166;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Purple;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 190;
            Item.knockBack = 20f;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwordsMod)}/Assets/Sounds/Item/GiantExplosion");
            Item.shoot = ModContent.ProjectileType<SOTUV5Projectile>();
            Item.shootSpeed = 15f;
            Item.expert = true;
            Item.value = Item.sellPrice(platinum: 10);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV2");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV9");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverse");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV3");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV4");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV6");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV7");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV8");
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
            float spin = MathHelper.ToRadians(12f);
            for (int i = 0; i < 3; i++)
            {
                float offset = i - (6f - 1f) / 2f;
                Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(spin * offset), ProjectileID.MonkStaffT3_AltShot, damage / 19, knockback, player.whoAmI);
            }
            Projectile.NewProjectileDirect(source, position + velocity * 3, velocity, type, damage / 2, knockback, player.whoAmI);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.Ichor, 360);
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.OnFire, 360);
            target.AddBuff(BuffID.Poisoned, 360);
            target.AddBuff(BuffID.CursedInferno, 360);
            for (int i = 0; i < 10; i++)
            {
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), target.Center, Vector2.UnitX.RotatedBy(-i * MathHelper.TwoPi / 10f * i, Vector2.Zero), ProjectileID.InfluxWaver, hit.Damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}