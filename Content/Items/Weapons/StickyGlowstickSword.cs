using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class StickyGlowstickSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 46;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 15;
            Item.knockBack = 3.5F;
            Item.UseSound = SoundID.Item1;
            Item.value = 12000;
            Item.DamageType = DamageClass.Melee;
        }

        public override bool? UseItem(Player player)
        {
            Lighting.AddLight(player.itemLocation, Color.Blue.ToVector3());
            return null;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White with { A = 0 };

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }
    }
}
