using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class InnosWrath : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Pulses with light energy of Innos");
        }

        public override void SetDefaults()
        {
            Item.width = 124;
            Item.height = 124;
            Item.scale = 1.125F;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 100;
            Item.knockBack = 12f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(gold: 6);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360);
            target.AddBuff(BuffID.Ichor, 360);
            target.AddBuff(BuffID.Venom, 360);
        }
    }
}