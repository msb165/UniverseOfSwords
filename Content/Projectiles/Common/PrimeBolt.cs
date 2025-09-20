using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    internal class PrimeBolt : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetDefaults()
        {
            Projectile.Size = new(6);
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 200;
            Projectile.usesIDStaticNPCImmunity = true;
        }

        public override void AI()
        {
            if (Projectile.ai[0] >= 0f)
            {
                Projectile.ai[0]++;
            }
            if (Projectile.ai[0] > Main.rand.Next(4, 10) && Main.myPlayer == Projectile.owner)
            {
                Projectile.ai[0] = -1f;
                float velLength = Projectile.velocity.Length();
                int projAmount = Main.rand.Next(0, 3);
                Vector2 projVel = Vector2.Normalize(Projectile.velocity) * 2f;
                for (int i = 0; i < projAmount; i++)
                {
                    Vector2 spawnVel = Vector2.Normalize(Utils.RandomVector2(Main.rand, -100f, 101f)) + projVel;
                    spawnVel.Normalize();
                    spawnVel *= velLength;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, spawnVel, Type, Projectile.damage, Projectile.knockBack, Projectile.owner, -1f);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.TheDestroyer);
                dust.position = Projectile.Center - Projectile.velocity / 5f * i;
                dust.scale = 1.5f;
                dust.noGravity = true;
                dust.velocity *= 0f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                target.immune[Projectile.owner] = 3;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Damage();
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, 0, 0, DustID.TheDestroyer);
                dust.scale = 1.5f;
                dust.noGravity = true;
                dust.velocity *= 2f;
            }
        }
    }
}
