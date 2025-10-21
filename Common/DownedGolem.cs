using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UniverseOfSwords.Common
{
    public class DownedGolem : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedGolemBoss;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}
