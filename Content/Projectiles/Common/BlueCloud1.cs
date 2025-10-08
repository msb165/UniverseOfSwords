using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class BlueCloud1 : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Gore_{11}";

        public override void SetDefaults()
        {
            Projectile.Size = new(32);
            Projectile.aiStyle = ProjAIStyleID.ToxicCloud;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.scale = 1.125f;
            AIType = ProjectileID.ToxicCloud;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Projectile.ai[0]++;
                Projectile.netUpdate = true;
            }
        }
    }
}
