using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Items.Placeable;
using UniverseOfSwords.Content.Items.Weapons;

namespace UniverseOfSwords.Common.GlobalItems
{
    public class GlobalItemLoot : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            Conditions.IsHardmode hardmode = new();
            if (ItemID.Sets.BossBag[item.type] || ItemID.Sets.IsFishingCrate[item.type] || ItemID.Sets.IsFishingCrateHardmode[item.type])
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordMatter>(), 10, 1, 3));
            }
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
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PrimeSword>()));
                    break;
                case ItemID.TwinsBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TwinsSword>()));
                    break;
                case ItemID.DestroyerBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DestroyerSword>()));
                    break;
                case ItemID.GolemBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordShard>(), 5, 1, 6));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Golem>()));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SolBlade>(), 100));
                    break;
                case ItemID.FishronBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sharkron>()));
                    break;
                case ItemID.MoonLordBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarMaelstorm>(), 30));
                    break;
                case ItemID.PlanteraBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Executioner>()));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlackBar>(), 1, 15, 30));
                    break;
            }
        }
    }
}
