using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Items.Placeable;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TrueHorrormageddon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'There used to be a graveyard, now it is a crater'");
        }

        public override void SetDefaults()
        {
            Item.width = 128;
            Item.height = 128;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 182;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item71;
            Item.shoot = ProjectileID.DeathSickle;
            Item.shootSpeed = 20f;
            Item.value = Item.sellPrice(platinum: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                Vector2 spawnVel = Main.rand.NextVector2CircularEdge(200f, 200f);
                Vector2 spawnPos = player.Center - spawnVel;
                Dust dust = Dust.NewDustPerfect(spawnPos, DustID.Clentaminator_Green, Vector2.Zero);
                dust.position = spawnPos;
                dust.scale = 1f;
                dust.velocity = -Vector2.Normalize(dust.position - player.Center) * 8f;
                dust.noGravity = true;
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -60f), Vector2.UnitY * 6f);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Horrormageddon>())
                .AddIngredient(ModContent.ItemType<PowerOfTheGalactic>())
                .AddIngredient(ModContent.ItemType<GnomBlade>())
                .AddIngredient(ItemID.BrokenHeroSword, 10)
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 25)
                .AddIngredient(ModContent.ItemType<BlackBar>(), 25)
                .AddIngredient(ModContent.ItemType<LunarOrb>(), 3)
                .AddIngredient(ItemID.LunarBar, 80)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileID.Meowmere, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.InfernoFriendlyBlast, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.StarWrath, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y + 2, velocity.X, velocity.Y + 2, ProjectileID.Meowmere, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y + 2, velocity.X, velocity.Y + 2, ProjectileID.DeathSickle, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y + 2, velocity.X, velocity.Y + 2, ProjectileID.InfernoFriendlyBlast, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y + 2, velocity.X, velocity.Y + 2, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y + 2, velocity.X, velocity.Y + 2, ProjectileID.StarWrath, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y - 2, velocity.X, velocity.Y - 2, ProjectileID.Meowmere, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y - 2, velocity.X, velocity.Y - 2, ProjectileID.DeathSickle, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y - 2, velocity.X, velocity.Y - 2, ProjectileID.InfernoFriendlyBlast, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y - 2, velocity.X, velocity.Y - 2, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y - 2, velocity.X, velocity.Y - 2, ProjectileID.StarWrath, damage, knockback, player.whoAmI);
            return true;
        }
    }
}