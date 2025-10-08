using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class CaesarSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Et tu, Brute?'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 24;
            Item.knockBack = 5.4f;
            Item.UseSound = SoundID.Item1;
            Item.value = 45900;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }
    }
}