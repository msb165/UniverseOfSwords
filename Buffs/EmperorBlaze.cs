using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common.GlobalNPCs;

namespace UniverseOfSwordsMod.Buffs
{
	public class EmperorBlaze : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Emperor Blaze");
			// Description.SetDefault("Losing life");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<UniversePlayer>().eBlaze = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalNPCEffects>().eBlaze = true;
		}
	}
}