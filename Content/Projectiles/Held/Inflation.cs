using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class Inflation : BaseCustomSwing
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.Inflation>().Texture;

        public override float SwordLength => 90f;

        public override ref float SwingDirection => ref Projectile.ai[1];

        public override float BaseScale => 1.25f;
    }
}
