using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Nightmare : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;          
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.scale = 1f;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;                      
            Projectile.DamageType = DamageClass.Melee;                   
            Projectile.tileCollide = false;            
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        int attackTarget = -1;
        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Purple);
            dust.noGravity = true;
            dust.scale = 1f;

            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = MathHelper.WrapAngle(Projectile.velocity.ToRotation());

            if (Projectile.ai[0] == 0f && Projectile.alpha > 0)
            {
                Projectile.alpha -= 8;
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    Projectile.ai[0] = 1f;
                }
            }

            if (Projectile.ai[0] >= 1f)
            {
                Projectile.SimpleFadeOut(ai: 0, 20f);
            }

            FindNPCAndApplySpeed();
            FindFrame();
        }

        public void FindNPCAndApplySpeed()
        {
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CanBeChasedBy(this) && attackTarget == -1 && npc.Distance(Projectile.Center) < 200f)
                {
                    attackTarget = npc.whoAmI;
                }
            }

            if (attackTarget != -1)
            {
                Projectile.timeLeft = 2;
                Vector2 speed = Vector2.Normalize(Main.npc[attackTarget].Center - Projectile.Center);
                Projectile.velocity = (Projectile.velocity * 40f + speed * 10f) / 41f;
            }
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

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.PurpleTorch, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 4f;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            if (UniverseUtils.IsAValidTarget(target) && Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.ShadowFlame, 800);
                owner.Heal(2);
            }
        }

        public override bool PreDraw(ref Color lightColor) //this is where the animation happens
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = new(texture.Width / 2 + texture.Width / 4, sourceRectangle.Height / 2);
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color trailColor = drawColor;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRectangle, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
