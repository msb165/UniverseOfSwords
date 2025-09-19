using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class WaterBoltSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 64;
            Item.useAnimation = 32;
            Item.damage = 15;
            Item.knockBack = 6f;
            Item.shoot = ModContent.ProjectileType<WateryBolt>();
            Item.shootSpeed = 2.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 4, silver: 85);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}