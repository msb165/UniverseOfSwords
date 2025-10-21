using Terraria.ModLoader;

namespace UniverseOfSwords.Common
{
    public class UniverseGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool decreasedDamage = false;
    }
}
