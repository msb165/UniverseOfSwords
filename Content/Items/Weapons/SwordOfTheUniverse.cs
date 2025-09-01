using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SwordOfTheUniverse : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Universe");
            /* Tooltip.SetDefault("'This sword doesn't swing. It lifts the Universe towards the blade'"
			    + "\nHas changeable forms"); */
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 274;
            Item.height = 274;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Purple;
            Item.crit = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 15;
            Item.damage = 190;
            Item.knockBack = 20f;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwordsMod)}/Assets/Sounds/Item/GiantExplosion");
            Item.shoot = ModContent.ProjectileType<SOTUProjectile1>();
            Item.shootSpeed = 15f;
            Item.expert = true;
            Item.value = Item.sellPrice(platinum: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV2");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV9");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV3");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV4");
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SwordOfTheUniverseV5");
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
            /*float spin = MathHelper.ToRadians(6f);
            for (int i = 0; i < 6; i++)
            {
                float offset = i - (6f - 1f) / 2f;
                Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(spin * offset) / 10, ProjectileID.VortexBeaterRocket, damage / 19, knockback, player.whoAmI);
            }
            Projectile.NewProjectileDirect(source, position + velocity * 3, velocity / 2 , type, damage / 2, knockback, player.whoAmI);*/
            Projectile.NewProjectileDirect(source, position + velocity * 4f, velocity / 2 , type, damage / 2, knockback, player.whoAmI);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (NPCID.Sets.CountsAsCritter[target.type] || target.immortal || !target.active)
            {
                return;
            }
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