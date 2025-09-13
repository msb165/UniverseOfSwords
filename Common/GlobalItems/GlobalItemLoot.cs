using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Items.Weapons;

namespace UniverseOfSwordsMod.Common.GlobalItems
{
    public class GlobalItemLoot : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            Conditions.IsHardmode hardmode = new();
            switch (item.type)
            {
                case ItemID.CorruptFishingCrate:
                case ItemID.CrimsonFishingCrate:
                case ItemID.DungeonFishingCrate:
                case ItemID.FloatingIslandFishingCrate:
                case ItemID.FrozenCrate:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordMatter>(), 25, 1, 3));
                    break;
                case ItemID.GoldenCrate:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordMatter>(), 25, 1, 3));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MasterSword>(), 30));
                    break;
                case ItemID.GoldenCrateHard:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordMatter>(), 25, 1, 3));
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MasterSword>(), 15));
                    break;
                case ItemID.CorruptFishingCrateHard:
                case ItemID.CrimsonFishingCrateHard:
                case ItemID.DungeonFishingCrateHard:
                case ItemID.FloatingIslandFishingCrateHard:
                case ItemID.FrozenCrateHard:
                    break;
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
            }
        }
    }
}
