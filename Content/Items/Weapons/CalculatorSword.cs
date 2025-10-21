using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class CalculatorSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Sprite was made in the calculator. True story.");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 52;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 18;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(silver: 20);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.holdStyle = 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), Vector2.UnitY * 4f );
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
            if (Main.myPlayer == player.whoAmI)
            {
                foreach (NPC npc in Main.ActiveNPCs)
                {
                    if (npc.active && npc.Distance(Main.MouseWorld) < 16f && Main.mouseRight && Main.mouseRightRelease)
                    {
                        Main.NewText($"Calculating...\nNPC's name: {npc.FullName}\nNPC's defense: {npc.defense}\nNPC's current HP: {npc.life}\nNPC's damage: {npc.damage}\n" +
                            $"Possible number of hits needed to defeat this enemy: {MathF.Ceiling(((float)npc.life / (float)Item.damage))}");
                    }
                }
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.WhiteTorch, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
