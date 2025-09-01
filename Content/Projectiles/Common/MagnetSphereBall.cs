using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class MagnetSphereBall : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.MagnetSphereBall}";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(38);
            Projectile.timeLeft = 900;
            Projectile.light = 0.5f;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            if (Projectile.velocity.Length() > 3f)
            {
                Projectile.velocity *= 0.98f;
            }
            FindFrame();
            MagnetSphere_TryAttacking();
            Projectile.SimpleFadeOut(ai: 0, 45f);
        }

        public void FindFrame()
        {
            if ((++Projectile.frameCounter) / 2 >= Main.projFrames[Type]) 
            {
                Projectile.frameCounter = 0; 
                if (++Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.frame = 0;
                }
            }
        }

        private void MagnetSphere_TryAttacking()
        {
            int[] targetIndexes = new int[20];
            int num = 0;
            float maxDistance = 300f;
            bool foundTarget = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (!npc.CanBeChasedBy(this))
                {
                    continue;
                }
                Vector2 targetPos = npc.Center;
                if (MathF.Abs(Projectile.Center.X) - targetPos.X + MathF.Abs(Projectile.Center.Y - targetPos.Y) < maxDistance && Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1))
                {
                    if (num < 20)
                    {
                        targetIndexes[num] = npc.whoAmI;
                        num++;
                    }
                    foundTarget = true;
                }
            }
            if (Projectile.timeLeft < 30)
            {
                foundTarget = false;
            }
            if (foundTarget)
            {
                int selectedTarget = Main.rand.Next(num);
                selectedTarget = targetIndexes[selectedTarget];
                Vector2 targetPos = Main.npc[selectedTarget].Center;
                Projectile.localAI[0]++;
                if (Projectile.localAI[0] > 8f)
                {
                    Projectile.localAI[0] = 0f;
                    Vector2 vector = Projectile.Center + Projectile.velocity * 4f;
                    Vector2 targetVel = Vector2.Normalize(targetPos - vector) * 6f;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), vector, targetVel, ModContent.ProjectileType<SphereLaser>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D texture2 = TextureAssets.Projectile[ProjectileID.StardustTowerMark].Value;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2;
            Vector2 originGlow = texture2.Size() / 2;
            Color drawColor = Color.White with { A = 170 } * Projectile.Opacity;
            Color glowColor = Color.DarkCyan with { A = 40 } * Projectile.Opacity;

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture2, Projectile.Center - Main.screenPosition, null, glowColor * 0.5f, Projectile.rotation, originGlow, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
