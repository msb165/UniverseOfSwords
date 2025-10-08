using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class StarDestroyerProj : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 5;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            if (Projectile.ai[1] != -1f && Projectile.position.Y > Projectile.ai[1])
            {
                Projectile.tileCollide = true;
            }
            if (Projectile.position.HasNaNs())
            {
                Projectile.Kill();
                return;
            }
            bool hasSolidTile = WorldGen.SolidTile(Framing.GetTileSafely(Projectile.position.ToTileCoordinates16()));
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Vortex);
                dust.scale = 1f;
                dust.position = Projectile.Center - Projectile.velocity / 3f * i;
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;
                if (hasSolidTile)
                {
                    dust.noLight = true;
                }
            }

            if (Projectile.ai[1] == -1f)
            {
                Projectile.ai[0]++;
                Projectile.velocity = Vector2.Zero;
                Projectile.tileCollide = false;
                Projectile.penetrate = -1;
                Projectile.Resize(140, 140);
                Projectile.alpha -= 10;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
                if (++Projectile.frameCounter >= Projectile.MaxUpdates * 3)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                }
                if (Projectile.ai[0] >= Main.projFrames[Type] * Projectile.MaxUpdates * 3)
                {
                    Projectile.Kill();
                }
                return;
            }
            Projectile.alpha = 255;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[Projectile.owner] = 0;
            if (Projectile.ai[1] != -1f)
            {
                Projectile.ai[0] = 0f;
                Projectile.ai[1] = -1f;
                Projectile.netUpdate = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] = 0f;
            Projectile.ai[1] = -1f;
            Projectile.netUpdate = true;
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnKill(int timeLeft)
        {
            bool flag2 = WorldGen.SolidTile(Framing.GetTileSafely(Projectile.position.ToTileCoordinates16()));
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
            }
            for (int j = 0; j < 4; j++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Vortex, 0f, 0f, 0, default(Color), 2.5f);
                dust.noGravity = true;
                Dust dust2 = dust;
                dust2.velocity *= 3f;
                if (flag2)
                {
                    dust.noLight = true;
                }
                dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Vortex, 0f, 0f, 100, default(Color), 1.5f);
                dust2 = dust;
                dust2.velocity *= 2f;
                dust.noGravity = true;
                if (flag2)
                {
                    dust.noLight = true;
                }
            }
            int gore = Gore.NewGore(Projectile.GetSource_Death(), Projectile.position + new Vector2((float)(Projectile.width * Main.rand.Next(100)) / 100f, (float)(Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
            Gore gore2 = Main.gore[gore];
            gore2.velocity *= 0.3f;
            Main.gore[gore].velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
            Main.gore[gore].velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            Main.instance.LoadProjectile(ProjectileID.LunarFlare);
            Texture2D texture = TextureAssets.Projectile[ProjectileID.LunarFlare].Value;
            Rectangle sourceRect = texture.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
            Vector2 origin = sourceRect.Size() / 2;
            Color drawColor = Color.White with { A = (byte)(127 - Projectile.alpha / 2) } * Projectile.Opacity;

            Main.spriteBatch.Draw(texture, drawPos, sourceRect, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            return base.PreDraw(ref lightColor);
        }
    }
}
