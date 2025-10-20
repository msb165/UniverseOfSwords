using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TheStinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots deadly Stingers");
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 23;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<Stinger>();
            Item.shootSpeed = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 50);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(4))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.Chlorophyte, 1.25f);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 100)
                .AddIngredient(ItemID.Vine, 1)
                .AddIngredient(ItemID.Stinger, 14)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}