using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Base;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class StoneShortsword : BaseShortSword
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.StoneShortsword>().Texture;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(target.Center, Projectile.width, Projectile.height, DustID.Stone);
            }
        }
    }
}
