using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwords.Common.Detours
{
    public partial class DetourChanges : ModSystem
    {
        public override void OnModLoad()
        {
            On_Player.ItemCheck_GetMeleeHitbox += On_Player_ItemCheck_GetMeleeHitbox;
            On_Player.ItemCheck_OwnerOnlyCode += AddProjectileReflection;
            On_NPC.AI_001_Slimes_GenerateItemInsideBody += On_NPC_AI_001_Slimes_GenerateItemInsideBody;
        }
    }
}
