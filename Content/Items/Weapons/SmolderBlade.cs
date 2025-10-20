using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class SmolderBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 42;
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 30;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 50);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Common.SmolderBlade>();
            Item.shootSpeed = 2.5f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}