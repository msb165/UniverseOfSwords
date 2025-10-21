using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class CrystalExcalibur : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Sword forged in heaven'");
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.5f;
        }

        public override void SetDefaults()
        {
            Item.width = 84;
            Item.height = 84;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.damage = 150;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 20);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<CrystalExcaliburEnergy>();
            Item.noMelee = true;
            Item.shootsEveryUse = true;
            Item.GetGlobalItem<ReflectionChance>().reflectChance = 8;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;


        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.IceTorch, 2f, (int)(90 * Item.scale), (int)(126 * Item.scale));
                UniverseUtils.SpawnRotatedDust(player, DustID.PinkTorch, 2f);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, Item.damage, 4f, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "OmegaExcalibur");
            recipe.AddIngredient(null, "AlphaExcalibur");
            recipe.AddIngredient(null, "AncientKatana");
            recipe.AddIngredient(ItemID.CrystalShard, 20);
            recipe.AddIngredient(ItemID.PixieDust, 15);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.DarkShard);
            recipe.AddIngredient(ItemID.LightShard);
            recipe.AddIngredient(ModContent.ItemType<MartianSaucerCore>());
            recipe.AddIngredient(null, "Orichalcon");
            recipe.AddIngredient(ModContent.ItemType<UpgradeMatter>(), 150);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}