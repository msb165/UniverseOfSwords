using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Consumables;
using UniverseOfSwords.Content.Items.Weapons;

namespace UniverseOfSwords.Common.Detours
{
    public partial class DetourChanges
    {
        private int On_NPC_AI_001_Slimes_GenerateItemInsideBody(On_NPC.orig_AI_001_Slimes_GenerateItemInsideBody orig, bool isBallooned)
        {
            int chance = Main.rand.Next(4);
            if (isBallooned)
            {
                return Main.rand.Next(13) switch
                {
                    1 => ItemID.KiteBlueAndYellow,
                    2 => ItemID.KiteRed,
                    3 => ItemID.KiteRedAndYellow,
                    4 => ItemID.KiteYellow,
                    5 => ItemID.KiteBunny,
                    6 => ItemID.KiteGoldfish,
                    7 or 8 or 9 => ItemID.PaperAirplaneA,
                    10 or 11 or 12 => ItemID.PaperAirplaneB,
                    _ => ItemID.KiteBlue,
                };
            }
            return chance switch
            {
                0 => Main.rand.Next(7) switch
                {
                    0 => ItemID.SwiftnessPotion,
                    1 => ItemID.IronskinPotion,
                    2 => ItemID.SpelunkerPotion,
                    3 => ItemID.MiningPotion,
                    4 => ModContent.ItemType<LesserMeleePowerPotion>(),
                    _ => (Main.netMode != NetmodeID.SinglePlayer && Main.rand.NextBool(2)) ? ItemID.WormholePotion : ItemID.RecallPotion,
                },
                1 => Main.rand.Next(5) switch
                {
                    0 => ItemID.Torch,
                    1 => ItemID.Bomb,
                    2 => ItemID.Rope,
                    3 => ModContent.ItemType<GelBlade>(),
                    _ => ItemID.Heart,
                },
                2 => Main.rand.NextBool(2) ? Main.rand.Next(ItemID.IronOre, ItemID.CopperWatch) : Main.rand.Next(ItemID.TinOre, ItemID.TinBar),
                _ => Main.rand.Next(3) switch
                {
                    0 => ItemID.CopperCoin,
                    1 => ItemID.SilverCoin,
                    _ => ItemID.GoldCoin,
                },
            };
        }
    }
}
