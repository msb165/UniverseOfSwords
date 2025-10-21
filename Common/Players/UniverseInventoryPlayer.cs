using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Weapons;

namespace UniverseOfSwords.Common.Players
{
    public class UniverseInventoryPlayer : ModPlayer
    {
        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
        {
            if (ModContent.GetInstance<UniverseConfig>().starterSwords)
            {
                itemsByMod["Terraria"].RemoveAll(item => item.type is ItemID.CopperShortsword or ItemID.IronShortsword);
            }
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (ModContent.GetInstance<UniverseConfig>().starterSwords)
            {
                return [];
            }
            return [
            new Item(Utils.SelectRandom(Main.rand,
                [
                    ModContent.ItemType<DirtSword>(),
                    ModContent.ItemType<StoneSword>(),
                    ModContent.ItemType<StoneShortsword>()
                ]))
            ];
        }
    }
}
