using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common.GlobalNPCs;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Buffs
{
    public class TrueSlow : ModBuff
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Buff_{BuffID.Slow}";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GlobalNPCEffects>().slow = true;
        }
    }
}
