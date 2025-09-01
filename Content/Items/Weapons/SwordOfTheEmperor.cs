using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;
using UniverseOfSwordsMod.Content.Items.Materials;

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
            Item.Size = new(170);
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.damage = 100;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item74;
            Item.value = 0;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 200)
                .AddIngredient(ItemID.HallowedBar, 4000)
                .AddIngredient(ItemID.BrokenHeroSword, 16)
                .AddIngredient(ItemID.EnchantedSword, 4)
                .AddIngredient(ItemID.Arkhalis, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<UpgradeMatter>(), 200)
                .AddIngredient(ItemID.HallowedBar, 4000)
                .AddIngredient(ItemID.BrokenHeroSword, 16)
                .AddIngredient(ItemID.EnchantedSword, 4)
                .AddIngredient(ItemID.Terragrim, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(8000))
            {
                target.AddBuff(ModContent.BuffType<EmperorBlaze>(), 100);
            }
            target.AddBuff(BuffID.OnFire, 300);
        }
    }
}