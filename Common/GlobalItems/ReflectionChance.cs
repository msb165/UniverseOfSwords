using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace UniverseOfSwords.Common.GlobalItems
{
    public class ReflectionChance : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public int reflectChance = 0;

        public override void SetDefaults(Item entity)
        {
            if ((entity.DamageType == DamageClass.Melee || entity.DamageType == DamageClass.MeleeNoSpeed) && entity.useStyle == ItemUseStyleID.Swing && entity.axe <= 0 && entity.pick <= 0 && reflectChance == 0)
            {
                reflectChance = 4;
            }

            switch (entity.type)
            {
                case ItemID.PlatinumBroadsword or ItemID.GoldBroadsword:
                    reflectChance = 3;
                    break;
                case ItemID.Cutlass:
                    reflectChance = 5;
                    break;
                case ItemID.FetidBaghnakhs or ItemID.BladedGlove:
                    reflectChance = 10;
                    break;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if ((item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed) && item.useStyle == ItemUseStyleID.Swing && item.axe <= 0 && item.pick <= 0 && reflectChance > 0)
            {
                string coloredText = Language.GetTextValue("Mods.UniverseOfSwords.Misc.ReflectionChance");
                TooltipLine reflectionChance = new(Mod, "ReflectChance", $"[c/BC2CFE:{reflectChance}%] {coloredText}");
                tooltips.Add(reflectionChance);
            }
        }
    }
}
