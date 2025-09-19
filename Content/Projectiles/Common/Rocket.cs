using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Rocket : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.RocketII}";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (MathF.Abs(Projectile.velocity.X) >= 8f || MathF.Abs(Projectile.velocity.Y) >= 8f)
            {
                for (int n = 0; n < 2; n++)
                {
                    float num23 = 0f;
                    float num24 = 0f;
                    if (n == 1)
                    {
                        num23 = Projectile.velocity.X * 0.5f;
                        num24 = Projectile.velocity.Y * 0.5f;
                    }
                    int num25 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num23, Projectile.position.Y + 3f + num24) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, DustID.Torch, 0f, 0f, 100);
                    Main.dust[num25].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                    Main.dust[num25].velocity *= 0.2f;
                    Main.dust[num25].noGravity = true;
                    if (Main.dust[num25].type == 152)
                    {
                        Main.dust[num25].scale *= 0.5f;
                        Main.dust[num25].velocity += Projectile.velocity * 0.1f;
                    }
                    else if (Main.dust[num25].type == 35)
                    {
                        Main.dust[num25].scale *= 0.5f;
                        Main.dust[num25].velocity += Projectile.velocity * 0.1f;
                    }
                    else if (Main.dust[num25].type == Dust.dustWater())
                    {
                        Main.dust[num25].scale *= 0.65f;
                        Main.dust[num25].velocity += Projectile.velocity * 0.1f;
                    }
                    num25 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num23, Projectile.position.Y + 3f + num24) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, DustID.Smoke, 0f, 0f, 100, default, 0.5f);
                    Main.dust[num25].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                    Main.dust[num25].velocity *= 0.05f;
                }
            }
            if (Math.Abs(Projectile.velocity.X) < 15f && Math.Abs(Projectile.velocity.Y) < 15f)
            {
                Projectile.velocity *= 1.1f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Resize(128, 128);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile.Resize(22, 22);
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                Dust dust2 = Main.dust[dust];
                dust2.velocity *= 1.4f;
            }
            for (int j = 0; j < 20; j++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3.5f);
                Main.dust[dust].noGravity = true;
                Dust dust2 = Main.dust[dust];
                dust2.velocity *= 7f;
                dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                dust2 = Main.dust[dust];
                dust2.velocity *= 3f;
            }
            for (int k = 0; k < 2; k++)
            {
                float speedMultiplier = 0.4f;
                if (k == 1)
                {
                    speedMultiplier = 0.8f;
                }
                int gore1 = Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
                Gore gore2 = Main.gore[gore1];
                gore2.velocity *= speedMultiplier;
                Main.gore[gore1].velocity.X += 1f;
                Main.gore[gore1].velocity.Y += 1f;
                gore1 = Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
                gore2 = Main.gore[gore1];
                gore2.velocity *= speedMultiplier;
                Main.gore[gore1].velocity.X -= 1f;
                Main.gore[gore1].velocity.Y += 1f;
                gore1 = Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
                gore2 = Main.gore[gore1];
                gore2.velocity *= speedMultiplier;
                Main.gore[gore1].velocity.X += 1f;
                Main.gore[gore1].velocity.Y -= 1f;
                gore1 = Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
                gore2 = Main.gore[gore1];
                gore2.velocity *= speedMultiplier;
                Main.gore[gore1].velocity.X -= 1f;
                Main.gore[gore1].velocity.Y -= 1f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
    }
}
