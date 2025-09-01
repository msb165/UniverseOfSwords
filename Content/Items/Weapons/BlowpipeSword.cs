using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BlowpipeSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Watch out for your wrists'");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 36;
            Item.useAnimation = 23;
            Item.damage = 16;
            Item.knockBack = 3.5F;
            Item.UseSound = SoundID.Item17;
            Item.shoot = ProjectileID.Seed;
            Item.shootSpeed = 3;
            Item.value = Item.sellPrice(silver: 40);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}