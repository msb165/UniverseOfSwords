using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class IchorBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 38;
            Item.useAnimation = 25;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Orange;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.damage = 26;
            Item.knockBack = 4f;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.shoot = ModContent.ProjectileType<IchorFlurry>();
            Item.shootSpeed = 15f;
            Item.value = Item.sellPrice(gold: 5);
        }
    }
}
