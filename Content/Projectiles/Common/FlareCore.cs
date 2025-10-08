using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class FlareCore : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<ScarletFlareCore>().Texture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 100;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.ai[1] != -1f && Main.npc[(int)Projectile.ai[1]].active)
            {
                Projectile.position += Main.npc[(int)Projectile.ai[1]].velocity;
            }
            if (Projectile.ai[0] == 0f)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 spawnRot = (-Vector2.UnitY.RotatedBy(i * MathHelper.TwoPi / 10f) * new Vector2(2f, 8f)).RotatedBy(Projectile.velocity.ToRotation());
                    Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.LifeDrain, Vector2.Zero);
                    dust.position = Projectile.Center + spawnRot;
                    dust.velocity = spawnRot.SafeNormalize(Vector2.UnitY);
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
                Projectile.ai[0] = 1f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain);
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-Projectile.width / 2 + 2, Projectile.width / 2 - 2) - Projectile.velocity / 3f * i;
                dust.velocity *= 0.1f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            Projectile.SimpleFadeOut(ai: 2, 10f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            if (UniverseUtils.IsAValidTarget(target) && Main.myPlayer == Projectile.owner)
            {
                int rand = Main.rand.Next(2);
                if (rand == 0)
                {
                    target.AddBuff(BuffID.OnFire, 700);
                }
                else if (rand == 1)
                {
                    UniverseUtils.Spawn.VampireHeal(damageDone, target.Center, target, owner);
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Resize(200, 200);
            Projectile.Damage();
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, Scale: 2.5f);
                dust.velocity *= 1.5f;
                dust.noGravity = true;
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, Scale: 1.5f);
                dust2.velocity *= 1.5f;
                dust2.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = texture.Size() / 2;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            float timer = (float)Main.timeForVisualEffects / 30f;
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + (Vector2.UnitY * 4f).RotatedBy(MathHelper.Pi * timer + MathHelper.Pi + MathHelper.PiOver2), null, drawColor with { A = 0 } * 0.25f, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + (Vector2.UnitY * 4f).RotatedBy(MathHelper.TwoPi / 3f * timer), null, drawColor with { A = 0 } * 0.25f, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + (Vector2.UnitY * 4f).RotatedBy(MathHelper.Pi * timer), null, drawColor with { A = 0 } * 0.25f, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            return false;
        }
    }
}