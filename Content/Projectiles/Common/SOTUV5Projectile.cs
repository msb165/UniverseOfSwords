using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class SOTUV5Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sotu Projectile 5");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Type] = 120;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Type] = 2;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
            Projectile.penetrate = -1;
            Projectile.noEnchantmentVisuals = true;
        }

        public ref float Timer => ref Projectile.ai[0];
        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = Projectile.velocity.ToRotation();
            }
            float direction = (MathF.Cos(Projectile.localAI[0]) >= 0f).ToDirectionInt();
            if (Projectile.ai[1] <= 0f)
            {
                direction *= -1f;
            }
            Timer++;
            Vector2 spinningpoint = (direction * (Timer / 30f * MathHelper.TwoPi - MathHelper.PiOver2)).ToRotationVector2();
            spinningpoint.Y *= (float)Math.Sin(Projectile.ai[1]);
            if (Projectile.ai[1] <= 0f)
            {
                spinningpoint.Y *= -1f;
            }
            spinningpoint = spinningpoint.RotatedBy(Projectile.localAI[0]);
            if (Timer < 30f)
            {
                Projectile.velocity += 20f * spinningpoint;
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4;  
            if (Projectile.direction == -1)
            {
                Projectile.rotation += MathHelper.PiOver2;
            }
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            Projectile.Center = Player.RotatedRelativePoint(Player.MountedCenter, addGfxOffY: false) + Vector2.Normalize(Projectile.velocity) + Vector2.Normalize(Projectile.rotation.ToRotationVector2());
            SetPlayerValues();
        }

        public void SetPlayerValues()
        {
            //Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            Player.ChangeDir(Projectile.direction);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = texture.Size() / 2;
            Color drawColor = Color.White with { A = 40 } * Projectile.Opacity;
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