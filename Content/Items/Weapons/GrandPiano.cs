using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class GrandPiano : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grand Piano");
            // Tooltip.SetDefault("'Rage Quit - Horrior'");
        }

        public override void SetDefaults()
        {
            Item.width = 142;
            Item.height = 142;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Orange;
            Item.crit = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 100;
            Item.knockBack = 8f;
            Item.UseSound = new Terraria.Audio.SoundStyle($"{UniverseUtils.SoundsPath}Item/GrandPiano");
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 20;
            Item.value = Item.sellPrice(gold: 30);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LivingWoodPiano, 1)
                .AddIngredient(ItemID.CactusPiano, 1)
                .AddIngredient(ItemID.EbonwoodPiano, 1)
                .AddIngredient(ItemID.RichMahoganyPiano, 1)
                .AddIngredient(ItemID.PalmWoodPiano, 1)
                .AddIngredient(ItemID.BorealWoodPiano, 1)
                .AddIngredient(ItemID.Piano, 1)
                .AddIngredient(ModContent.ItemType<PianoSword2>())
                .AddIngredient(ModContent.ItemType<PianoSword4>())
                .AddTile(TileID.Autohammer)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 1, velocity.Y + 1, ProjectileID.CrystalBullet, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 1, velocity.Y - 1, ProjectileID.VampireKnife, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 2, velocity.Y + 2, ProjectileID.HeatRay, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 2, velocity.Y - 2, ProjectileID.BulletHighVelocity, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.FrostburnArrow, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 3, velocity.Y + 3, ProjectileID.FireArrow, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 3, velocity.Y - 3, ProjectileID.FlaironBubble, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 4, velocity.Y + 4, ProjectileID.CursedArrow, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 4, velocity.Y - 4, ProjectileID.IchorArrow, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 5, velocity.Y + 5, ProjectileID.Stake, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 5, velocity.Y - 5, ProjectileID.JavelinFriendly, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 6, velocity.Y + 6, ProjectileID.JackOLantern, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 6, velocity.Y - 6, ProjectileID.Mushroom, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 7, velocity.Y + 7, ProjectileID.LostSoulFriendly, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 7, velocity.Y - 7, ProjectileID.ShadowBeamFriendly, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 8, velocity.Y + 8, ProjectileID.InfernoFriendlyBolt, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 8, velocity.Y - 8, ProjectileID.HellfireArrow, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 9, velocity.Y + 9, ProjectileID.MeteorShot, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 9, velocity.Y - 9, ProjectileID.Bone, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X + 10, velocity.Y + 10, ProjectileID.FallingStar, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X - 10, velocity.Y - 10, ProjectileID.HolyArrow, damage, knockback, player.whoAmI);
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 360);
            target.AddBuff(BuffID.Electrified, 360);
            target.AddBuff(BuffID.Bleeding, 360);
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.ShadowFlame, 360);
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.Slimed, 360);
            target.AddBuff(BuffID.Venom, 360);
        }
    }
}