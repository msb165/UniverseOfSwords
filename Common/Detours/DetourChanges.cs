using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Common.Detours
{
    public partial class DetourChanges : ModSystem
    {
        public override void OnModLoad()
        {
            On_Player.ItemCheck_GetMeleeHitbox += On_Player_ItemCheck_GetMeleeHitbox;
            On_Player.ItemCheck_OwnerOnlyCode += AddProjectileReflection;
        }
    }
}
