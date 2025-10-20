using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class FreezeFireClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("FreezeFire Claw");
            // Tooltip.SetDefault("'Burns and freezes your enemies to death'");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 56;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 50;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 5);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.DungeonSpirit, 2f);
                UniverseUtils.SpawnRotatedDust(player, DustID.Torch, 2f);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LivingFireBlock, 100)
                .AddIngredient(ItemID.FrostCore, 2)
                .AddIngredient(ItemID.LivingFireBlock, 25)
                .AddIngredient(ItemID.IceBlock, 150)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 200)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 360);
            target.AddBuff(BuffID.Frostburn, 360);
        }
    }
}