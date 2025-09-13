using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;
using static Terraria.Player;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class SwordOfTheMultiverse : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sword of the Multiverse");
            // Tooltip.SetDefault("'WARNING! Do NOT craft this Sword! Crafting it will break the game AND your sanity!'");
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 555;
            Item.height = 555;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Expert;
            Item.crit = 65;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 80000;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item1 with { Pitch = -1f };
            Item.shoot = ModContent.ProjectileType<SOTM>();
            Item.shootSpeed = 30f;
            Item.expert = true;
            Item.value = Item.sellPrice(platinum: 90);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        //public override bool CanShoot(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        //public override bool MeleePrefix() => true;

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center + new Vector2(12f * -player.direction, 4f);
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(64f * player.direction, -96f), new Vector2(12f * -player.direction, 4f));
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SOTM>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X + 10, velocity.Y + 10), ModContent.ProjectileType<SOTM>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X - 10, velocity.Y - 10), ModContent.ProjectileType<SOTM>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SOTUProjectile1>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X + 5, velocity.Y + 5), ModContent.ProjectileType<SOTUProjectile2>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X - 5, velocity.Y - 5), ModContent.ProjectileType<SOTUProjectile3>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X + 8, velocity.Y + 8), ModContent.ProjectileType<SOTUV4Projectile>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X - 8, velocity.Y - 8), ModContent.ProjectileType<SOTUV5Projectile>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X + 12, velocity.Y + 12), ModContent.ProjectileType<SOTUV6Projectile>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X - 13, velocity.Y - 13), ModContent.ProjectileType<SOTU7>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X + 12, velocity.Y + 12), ModContent.ProjectileType<SOTU8>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, new(velocity.X - 10, velocity.Y - 10), ModContent.ProjectileType<SOTUV9Projectile>(), damage, knockback, player.whoAmI);
            //Projectile.NewProjectile(source, position, Vector2.Normalize(velocity), ModContent.ProjectileType<Projectiles.Held.SwordOfTheMultiverse>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverse>())
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV2>())
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV3>())
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV4>())
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV5>())
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV6>())
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV7>())
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV8>())
                .AddIngredient(ModContent.ItemType<SwordOfTheUniverseV9>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<EmperorBlaze>(), 1000);
        }
    }
}
