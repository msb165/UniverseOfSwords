using System.Collections.Generic;
using Terraria;
using Terraria.ID;
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
                reflectChance = 2;
            }

            switch (entity.type)
            {
                case ItemID.PlatinumBroadsword or ItemID.GoldBroadsword:
                    reflectChance = 3;
                    break;
                case ItemID.PalladiumSword or ItemID.CobaltSword or ItemID.TitaniumSword or ItemID.AdamantiteSword:
                    reflectChance = 4;
                    break;
                case ItemID.Cutlass:
                    reflectChance = 5;
                    break;
                case ItemID.FetidBaghnakhs:
                    reflectChance = 10;
                    break;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if ((item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed) && item.useStyle == ItemUseStyleID.Swing)
            {
                string coloredText = "[c/AC1CEE:p][c/9C0CDE:a][c/8C0CBE:r][c/7C0CAE:r][c/6C0C9E:y] [c/AC1CEE:c][c/9C0CDE:h][c/8C0CBE:a][c/7C0CAE:n][c/6C0C9E:c][c/6C0C9E:e]";
                TooltipLine reflectionChance = new(Mod, "ReflectChance", $"[c/BC2CFE:{reflectChance}%] {coloredText}");
                tooltips.Add(reflectionChance);
            }
        }
    }
}
