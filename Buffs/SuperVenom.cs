using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalNPCs;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Buffs
{
    public class SuperVenom : ModBuff
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Buff_{BuffID.Poisoned}";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GlobalNPCEffects>().sVenom = true;
        }
    }
}
