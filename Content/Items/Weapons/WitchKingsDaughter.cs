using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class WitchKingsDaughter : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Witch King's Daughter");
            // Tooltip.SetDefault("Inflicts enemies with Cursed Flames");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.2F;
            Item.rare = 5;
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 54;
            Item.knockBack = 6.0F;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 5);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 600);
        }
    }
}
