using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class GreenSaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(68);
            Projectile.scale = 1.125f;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 300;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] > 10f && Main.rand.NextBool(3))
            {
                int maxDust = 6;
                for (int i = 0; i < maxDust; i++)
                {
                    Vector2 spawnPosOffset = Vector2.Normalize(Projectile.velocity) * Projectile.Size / 2f;
                    spawnPosOffset = spawnPosOffset.RotatedBy((double)(i - (maxDust / 2 - 1)) * MathHelper.Pi / maxDust) + Projectile.Center;
                    Vector2 spawnVel = ((float)(Main.rand.NextFloat() * MathHelper.Pi) - MathHelper.PiOver2).ToRotationVector2() * Main.rand.Next(3, 8);
                    int dust = Dust.NewDust(spawnPosOffset + spawnVel, 0, 0, DustID.Clentaminator_Green, spawnVel.X * 2f, spawnVel.Y * 2f, 100, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Dust dust2 = Main.dust[dust];
                    dust2.velocity /= 4f;
                    dust2 = Main.dust[dust];
                    dust2.velocity -= Projectile.velocity;
                }
                Projectile.alpha -= 5;
                if (Projectile.alpha < 50)
                {
                    Projectile.alpha = 50;
                }
                Projectile.rotation += Projectile.velocity.X * 0.1f;
                Lighting.AddLight(Projectile.Center, Color.Green.ToVector3());
            }
            int targetIndex = -1;
            Vector2 targetPos = Projectile.Center;
            float maxDistance = 500f;
            if (Projectile.localAI[0] > 0f)
            {
                Projectile.localAI[0]--;
            }
            if (Projectile.ai[0] == 0f && Projectile.localAI[0] == 0f)
            {
                foreach (NPC npc in Main.ActiveNPCs)
                {
                    if (npc.CanBeChasedBy(this) && (Projectile.ai[0] == 0f))
                    {
                        Vector2 npcCenter = npc.Center;
                        float distance = Vector2.Distance(npcCenter, targetPos);
                        if (distance < maxDistance && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                        {
                            maxDistance = distance;
                            targetPos = npcCenter;
                            targetIndex = npc.whoAmI;
                        }
                    }
                }
                if (targetIndex >= 0)
                {
                    Projectile.ai[0] = targetIndex + 1;
                    Projectile.netUpdate = true;
                }
            }
            if (Projectile.localAI[0] == 0f && Projectile.ai[0] == 0f)
            {
                Projectile.localAI[0] = 15f;
            }
            bool foundTarget = false;
            if (Projectile.ai[0] != 0f)
            {
                int index = (int)(Projectile.ai[0] - 1f);
                if (Main.npc[index].active && !Main.npc[index].dontTakeDamage && Main.npc[index].immune[Projectile.owner] == 0)
                {
                    float targetPosX = Main.npc[index].Center.X;
                    float targetPosY = Main.npc[index].Center.Y;
                    float distance = Math.Abs(Projectile.Center.X - targetPosX) + Math.Abs(Projectile.Center.Y - targetPosY);
                    if (distance < 1000f)
                    {
                        foundTarget = true;
                        targetPos = Main.npc[index].Center;
                    }
                }
                else
                {
                    Projectile.ai[0] = 0f;
                    foundTarget = false;
                    Projectile.netUpdate = true;
                }
            }
            if (foundTarget)
            {
                Vector2 v6 = targetPos - Projectile.Center;
                float projRot = Projectile.velocity.ToRotation();
                float targetRot = v6.ToRotation();
                double rotation = MathHelper.WrapAngle(targetRot - projRot);
                Projectile.velocity = Projectile.velocity.RotatedBy(rotation * 0.2f);
            }
            float velLength = Projectile.velocity.Length();
            Projectile.velocity.Normalize();
            Projectile.velocity *= velLength + 0.0025f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Main.myPlayer == Projectile.owner)
            {
                target.immune[Projectile.owner] = 3;
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, Color.SeaGreen, Projectile.owner, Projectile.damage, lerpToWhite: 0.75f);
            }
            target.AddBuff(ModContent.BuffType<SuperVenom>(), 300);
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
            Main.instance.LoadProjectile(ProjectileID.VortexVortexPortal);
            Texture2D texture = TextureAssets.Projectile[ProjectileID.VortexVortexPortal].Value;
            Vector2 origin = texture.Size() / 2;
            Color drawColor = Projectile.GetAlpha(lightColor) with { A = 170 };
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}