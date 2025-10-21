using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Nightlight : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 56;
            Item.scale = 1.3f;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 71;
            Item.knockBack = 6f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Common.Nightlight>();
            Item.shootSpeed = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 3);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.VilePowder, 1.5f, end: (int)(90 * Item.scale));
                UniverseUtils.SpawnRotatedDust(player, DustID.PinkTorch, 1.5f, end: (int)(90 * Item.scale));
            }
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += velocity * 2f - Vector2.UnitY * 24f;
            knockback /= 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrozenShard>());
            recipe.AddIngredient(ItemID.SoulofNight, 20);
            recipe.AddIngredient(ItemID.SoulofLight, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}