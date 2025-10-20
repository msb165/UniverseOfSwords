using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class FlamesBlast : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetDefaults()
        {
            Projectile.Size = new(260);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                Projectile.localAI[0]++;
            }
            for (int i = 0; i < 20; i++)
            {
                Vector2 spawnVel = Vector2.Normalize(Utils.RandomVector2(Main.rand, -10f, 11f)) * Main.rand.Next(6, 18);
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork);
                dust.noGravity = true;
                dust.scale = 2f;
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-10f, 11f);
                dust.velocity = spawnVel;
            }
        }
    }
}
