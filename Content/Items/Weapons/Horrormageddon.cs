using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class Horrormageddon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Where you see an army, I see a graveyard'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(128);
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 360;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item71;
            Item.shoot = ProjectileID.DeathSickle;
            Item.shootSpeed = 10;
            Item.value = Item.sellPrice(gold: 6, silver: 6, copper: 6);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            base.MeleeEffects(player, hitbox);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Doomsday>())
                .AddIngredient(ModContent.ItemType<StarMaelstorm>())
                .AddIngredient(ModContent.ItemType<Machine>())
                .AddIngredient(ModContent.ItemType<InnosWrath>())
                .AddIngredient(ModContent.ItemType<BeliarClaw>())
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 25)
                .AddIngredient(ModContent.ItemType<LunarOrb>())
                .AddIngredient(ItemID.LargeEmerald, 1)
                .AddIngredient(ItemID.Meowmere, 1)
                .AddIngredient(ItemID.TheHorsemansBlade, 1)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ProjectileID.Meowmere, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.InfernoFriendlyBlast, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.StarWrath, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
            return true;
        }
    }
}