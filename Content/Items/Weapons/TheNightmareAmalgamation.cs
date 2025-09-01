using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class TheNightmareAmalgamation : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The source of your nightmares'");
        }

        public override void SetDefaults()
        {
            Item.width = 110;
            Item.height = 110;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.damage = 150;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item71;
            Item.shoot = ModContent.ProjectileType<Nightmare>();
            Item.shootSpeed = 5f;
            Item.value = Item.sellPrice(gold: 30);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position - Vector2.UnitY * 80f + velocity * 4f, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.PurpleTorch, 2f, 30, 160);
            UniverseUtils.SpawnRotatedDust(player, DustID.PurpleTorch, 2f, 30, 160);
            Lighting.AddLight(player.Center, Color.Purple.ToVector3());
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            target.AddBuff(BuffID.ShadowFlame, 800);
            target.AddBuff(BuffID.Venom, 800);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CthulhuJudge>());
            recipe.AddIngredient(ModContent.ItemType<StickyGlowstickSword>());
            recipe.AddIngredient(ModContent.ItemType<TheEater>());
            recipe.AddIngredient(ItemID.BeeKeeper, 1);
            recipe.AddIngredient(null, "SwordOfPower", 1);
            recipe.AddIngredient(ItemID.BreakerBlade, 1);
            recipe.AddIngredient(null, "PrimeSword", 1);
            recipe.AddIngredient(null, "DestroyerSword", 1);
            recipe.AddIngredient(null, "TwinsSword", 1);
            recipe.AddIngredient(null, "Executioner", 1);
            recipe.AddIngredient(null, "Golem", 1);
            recipe.AddIngredient(null, "Doomsday", 1);
            recipe.AddIngredient(null, "Sharkron", 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CthulhuJudge>());
            recipe.AddIngredient(ModContent.ItemType<StickyGlowstickSword>());
            recipe.AddIngredient(ModContent.ItemType<TheBrain>());
            recipe.AddIngredient(ItemID.BeeKeeper, 1);
            recipe.AddIngredient(null, "SwordOfPower", 1);
            recipe.AddIngredient(ItemID.BreakerBlade, 1);
            recipe.AddIngredient(null, "PrimeSword", 1);
            recipe.AddIngredient(null, "DestroyerSword", 1);
            recipe.AddIngredient(null, "TwinsSword", 1);
            recipe.AddIngredient(null, "Executioner", 1);
            recipe.AddIngredient(null, "Golem", 1);
            recipe.AddIngredient(null, "Doomsday", 1);
            recipe.AddIngredient(null, "Sharkron", 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}