using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class RubyBolt : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";
        public override void SetDefaults()
        {
            Projectile.Size = new(10);
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.25f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity *= 0.3f;
            }
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int j = 0; j < 15; j++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 50, default, 1.25f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.scale *= 1.25f;
                dust2 = dust;
                dust2.velocity *= 0.5f;
            }
        }

    }
}
