using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace UniverseOfSwordsMod.Utilities
{
    public partial class UniverseUtils
    {
        public static bool IsAValidTarget(NPC target) => target.active && !NPCID.Sets.CountsAsCritter[target.type] && !target.immortal && target.chaseable && !target.dontTakeDamage; 
    }
}
