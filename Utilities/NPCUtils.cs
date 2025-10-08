using Terraria;
using Terraria.ID;

namespace UniverseOfSwords.Utilities
{
    public partial class UniverseUtils
    {
        public static bool IsAValidTarget(NPC target) => target.active && !target.CountsAsACritter && !target.immortal && target.chaseable && !target.dontTakeDamage; 
    }
}
