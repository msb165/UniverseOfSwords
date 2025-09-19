using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Grenade : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.Grenade}";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.Explosive[Type] = true;
            ProjectileID.Sets.TrailCacheLength[Type] = 5;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(14);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            if (Projectile.velocity.Y == 0 && Projectile.velocity.X != 0f)
            {
                Projectile.velocity.X *= 0.97f;
                if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                {
                    Projectile.velocity.X = 0f;
                    Projectile.netUpdate = true;
                }
            }
            Projectile.rotation += Projectile.velocity.X * 0.2f;
            Projectile.velocity.Y += 0.2f;
            for (int j = 0; j < 3; j++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.JungleSpore, Alpha: 100, Scale: 1.25f);
                dust.noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Resize(22, 22);
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Alpha: 100, Scale: 1.5f);
                Dust dust2 = dust;
                dust2.velocity *= 1.4f;
            }
            for (int j = 0; j < 20; j++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Alpha: 100, Scale: 3.5f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity *= 7f;
                dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Alpha: 100, Scale: 1.5f);
                dust2 = dust;
                dust2.velocity *= 3f;
            }
            for (int k = 0; k < 2; k++)
            {
                float speedMultiplier = 0.4f;
                if (k == 1)
                {
                    speedMultiplier = 0.8f;
                }
                Gore gore1 = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
                Gore gore2 = gore1;
                gore2.velocity *= speedMultiplier;
                gore1.velocity += Vector2.One;
                gore1 = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
                gore2 = gore1;
                gore2.velocity *= speedMultiplier;
                gore1.velocity += new Vector2(-1f, 1f);
                gore1 = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
                gore2 = gore1;
                gore2.velocity *= speedMultiplier;
                gore1.velocity += new Vector2(1f, -1f);
                gore1 = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
                gore2 = gore1;
                gore2.velocity *= speedMultiplier;
                gore1.velocity -= Vector2.One;
            }
        }

        public override void PrepareBombToBlow()
        {
            Projectile.Resize(128, 128);
            Projectile.knockBack = 8f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Projectile.GetAlpha(lightColor));
            return false;
        }
    }
}
