using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Weapons;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class IceBreakerProj : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<IceBreaker>().Texture;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 36000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.noEnchantmentVisuals = true;
        }

        public ref float Timer => ref Projectile.ai[0];
        public ref float Timer2 => ref Projectile.localAI[1];
        Player Player => Main.player[Projectile.owner];

        public override void AI()
        {
            Timer2--;
            if (Timer2 <= 30f)
            {
                Timer2 = 190f;
            }
            Timer++;
            if (Main.myPlayer == Projectile.owner && Player.controlUseTile && Timer >= 8f && Projectile.localAI[0] == 1f || (Main.myPlayer == Projectile.owner && Player.Distance(Projectile.Center) > 800f))
            {
                Projectile.ai[2] = 1f;
                Projectile.netUpdate = true;
            }
            if ((Main.myPlayer == Projectile.owner && Player.Distance(Projectile.Center) > 1200f))
            {
                Projectile.Kill();
            }
            if (Projectile.ai[2] == 1f)
            {
                BoomerangBehaviour();
                return;
            }
            NormalBehaviour();
        }

        public void BoomerangBehaviour()
        {
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 16;
                SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
            }
            Projectile.tileCollide = false;
            Projectile.rotation += 0.4f * Projectile.direction;
            Projectile.velocity = Projectile.Center.DirectionTo(Player.Center) * 8f;
            if (Projectile.Hitbox.Intersects(Player.Hitbox))
            {
                Projectile.Kill();
            }
        }

        public void NormalBehaviour()
        {
            Projectile.rotation = MathHelper.PiOver2 + MathHelper.PiOver4;
            Projectile.velocity.Y = UniverseUtils.Easings.EaseInBack(Timer / 50f) * 50f;
            // Prevents the projectile from getting stuck if there are solid tiles above the player.
            if (Projectile.Center.Y >= Player.position.Y && Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
            }
            Projectile.tileCollide = Projectile.localAI[0] == 1f;
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, 0, -8f, Scale: 2f);
            dust.noGravity = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.ai[2] == 1f)
            {
                return false;
            }

            if (Projectile.ai[1] == 0f)
            {
                SoundEngine.PlaySound(SoundID.DD2_GhastlyGlaiveImpactGhost, Projectile.position);
                SoundEngine.PlaySound(SoundID.Item37 with { Pitch = -0.25f }, Projectile.position);
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.position);
                for (int i = 0; i < 30; i++)
                {
                    Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height * 2, DustID.SpectreStaff, 0f, -8f, Scale: 3.5f);
                    dust2.noGravity = true;
                    dust2.velocity *= 1.5f;
                }
                Projectile.ai[1] = 1f;
            }
            Projectile.position += Projectile.velocity;
            Projectile.velocity = Vector2.Zero;
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            Vector2 velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * Projectile.scale;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center - velocity * 70f, Projectile.Center + velocity * 10f, 24f * Projectile.scale, ref _);
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
            behindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = Projectile.ai[2] == 1f ? new Vector2(12f * Projectile.scale, Projectile.width * 2f * Projectile.scale) : new Vector2(Projectile.width * 2f, 12f);
            Color drawColor = Projectile.GetAlpha(lightColor);
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            Color glowColor = Color.SkyBlue with { A = 100 };
            Color glowColor2 = glowColor * 0.5f;
            Vector2 glowPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Texture2D glowTexture = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 glowOrigin = glowTexture.Size() / 2f;
            float scaleAndColorMultiplier = Utils.GetLerpValue(15f, 30f, Timer2, clamped: true) * Utils.GetLerpValue(240f, 200f, Timer2, clamped: true) * (1f + 0.2f * (float)MathF.Cos(Main.GlobalTimeWrappedHourly % 30f * MathHelper.TwoPi * 3f)) * 0.8f;
            Vector2 star1Scale = new Vector2(0.5f, 3f) * scaleAndColorMultiplier;
            Vector2 star2Scale = new Vector2(0.5f, 2f) * scaleAndColorMultiplier;
            glowColor *= scaleAndColorMultiplier;
            glowColor2 *= scaleAndColorMultiplier;
            Main.EntitySpriteDraw(glowTexture, glowPos, null, glowColor, MathHelper.PiOver2, glowOrigin, star1Scale, SpriteEffects.None);
            Main.EntitySpriteDraw(glowTexture, glowPos, null, glowColor, 0f, glowOrigin, star2Scale, SpriteEffects.None);
            Main.EntitySpriteDraw(glowTexture, glowPos, null, glowColor2, MathHelper.PiOver2, glowOrigin, star1Scale * 0.6f, SpriteEffects.None);
            Main.EntitySpriteDraw(glowTexture, glowPos, null, glowColor2, 0f, glowOrigin, star2Scale * 0.6f, SpriteEffects.None);

            return false;
        }
    }
}
