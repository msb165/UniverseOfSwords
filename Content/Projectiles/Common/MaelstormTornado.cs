using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class MaelstormTornado : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.DD2ApprenticeStorm}";

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 210;
            Projectile.aiStyle = ProjAIStyleID.WisdomWhirlwind;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 600;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
        }

        public override void OnKill(int timeLeft)
        {
            bool activeSound = SoundEngine.TryGetActiveSound(SlotId.FromFloat(Projectile.localAI[1]), out ActiveSound sound);
            if (activeSound)
            {
                sound.Volume = 0f;
                sound.Stop();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float maxTime = 300f;
            float timer = Projectile.ai[0];
            float limit = MathHelper.Clamp(timer / 30f, 0f, 1f);
            if (timer > maxTime - 60f)
            {
                limit = MathHelper.Lerp(1f, 0f, (timer - (maxTime - 60f)) / 60f);
            }
            float num306 = 0.2f;
            Vector2 top = Projectile.Top;
            Vector2 bottom = Projectile.Bottom;
            Vector2 verticalCenter = new(0f, bottom.Y - top.Y);
            verticalCenter.X = verticalCenter.Y * num306;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle sourceRect = texture.Frame();
            Vector2 origin = sourceRect.Size() / 2f;
            float num307 = -(float)Math.PI / 20f * timer * (!(Projectile.velocity.X > 0f)).ToDirectionInt();
            SpriteEffects spriteEffects = ((Projectile.velocity.X > 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None);
            bool isVelocityPositive = Projectile.velocity.X > 0f;
            Vector2 spinningpoint = Vector2.UnitY.RotatedBy(timer * 0.14f);
            float num308 = 0f;
            float amount = 5.01f + timer / 150f * -0.9f;
            if (amount < 4.11f)
            {
                amount = 4.11f;
            }
            Color baseColor1 = new(160, 140, 100, 127);
            Color baseColor2 = new(140, 160, 255, 127);
            float num310 = timer % 60f;
            if (num310 < 30f)
            {
                baseColor2 *= Utils.GetLerpValue(22f, 30f, num310, clamped: true);
            }
            else
            {
                baseColor2 *= Utils.GetLerpValue(38f, 30f, num310, clamped: true);
            }
            bool notTransparent = baseColor2 != Color.Transparent;
            for (float i = (int)bottom.Y; i > (int)top.Y; i -= amount)
            {
                num308 += amount;
                float num312 = num308 / verticalCenter.Y;
                float num313 = num308 * MathHelper.TwoPi / -20f;
                if (isVelocityPositive)
                {
                    num313 *= -1f;
                }
                float num314 = num312 - 0.35f;
                Vector2 drawPos = spinningpoint.RotatedBy(num313);
                Vector2 vector72 = new(0f, num312 + 1f);
                vector72.X = vector72.Y * num306;
                Color drawColor1 = Color.Lerp(Color.Transparent, baseColor1, num312 * 2f);
                if (num312 > 0.5f)
                {
                    drawColor1 = Color.Lerp(Color.Transparent, baseColor1, 2f - num312 * 2f);
                }
                drawColor1.A = (byte)(drawColor1.A * 0.5f);
                drawColor1 *= limit;
                //drawPos *= vector72 * 100f;
                drawPos = Vector2.Zero;
                drawPos += new Vector2(bottom.X, i) - Main.screenPosition;
                if (notTransparent)
                {
                    Color drawColor2 = Color.Lerp(Color.Transparent, baseColor2, num312 * 2f);
                    if (num312 > 0.5f)
                    {
                        drawColor2 = Color.Lerp(Color.Transparent, baseColor2, 2f - num312 * 2f);
                    }
                    drawColor2.A = (byte)(drawColor2.A * 0.5f);
                    drawColor2 *= limit;
                    Main.EntitySpriteDraw(texture, drawPos, sourceRect, drawColor2, num307 + num313, origin, (1f + num314) * 0.8f, spriteEffects);
                }
                Main.EntitySpriteDraw(texture, drawPos, sourceRect, drawColor1, num307 + num313, origin, 1f + num314, spriteEffects);
            }
            return false;
        }
    }
}
