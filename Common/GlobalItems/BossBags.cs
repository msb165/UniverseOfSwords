using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Items.Weapons;

namespace UniverseOfSwordsMod.Common.GlobalItems
{
    public class BossBags : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            switch (item.type)
            {
                case ItemID.EyeOfCthulhuBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CthulhuJudge>()));
                    break;
                case ItemID.KingSlimeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StickyGlowstickSword>()));
                    break;
                case ItemID.EaterOfWorldsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheEater>()));
                    break;
                case ItemID.SkeletronBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordOfPower>()));
                    break;
                case ItemID.SkeletronPrimeBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<UpgradeMatter>(), 10, 1, 6));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PrimeSword>()));
                    break;
                case ItemID.TwinsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<UpgradeMatter>(), 10, 1, 6));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TwinsSword>()));
                    break;
                case ItemID.DestroyerBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<UpgradeMatter>(), 10, 1, 6));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DestroyerSword>()));
                    break;
                case ItemID.GolemBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordShard>(), 10, 1, 6));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<UpgradeMatter>(), 10, 1, 6));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Golem>()));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SolBlade>(), 100));
                    break;
                /*case ItemID.CultistBossBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Doomsday>()));
                    break;*/
                case ItemID.FishronBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<UpgradeMatter>(), 10, 1, 6));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sharkron>()));
                    break;

            }
        }
    }
}
