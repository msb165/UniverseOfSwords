using Terraria.ID;
using UniverseOfSwords.Content.Projectiles.Base;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class ViltethornTip : BaseVilethorn
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.VilethornTip}";

        public override bool CountAsBase => false;
    }
}
