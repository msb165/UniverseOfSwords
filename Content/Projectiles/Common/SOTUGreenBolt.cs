using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Dusts;
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class SOTUGreenBolt : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}NPC_{NPCID.CursedSkull}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            Main.projFrames[Type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(24);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 0;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            //Projectile.tileCollide = false;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.spriteDirection = Projectile.direction;
            Projectile.SimpleFadeOut(ai: 0, 15f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Zombie103, Projectile.position);
            Projectile.Resize(260, 260);
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
            }
            for (int j = 0; j < 30; j++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Green, 0f, 0f, 0, default, 2.5f);
                Main.dust[dust].noGravity = true;
                Dust dust2 = Main.dust[dust];
                dust2.velocity *= 3f;
                dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Green, 0f, 0f, 100, default, 1.5f);
                dust2 = Main.dust[dust];
                dust2.velocity *= 2f;
                Main.dust[dust].noGravity = true;
            }
            Projectile.Damage();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D textureGlow = TextureAssets.Projectile[ProjectileID.StardustTowerMark].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2;
            Vector2 originGlow = textureGlow.Size() / 2;
            Color drawColor = Color.Lime with { A = 100 } * Projectile.Opacity;
            Color trailColor = drawColor;
            Color trailColor2 = Color.LightGreen with { A = 0 } * Projectile.Opacity;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                trailColor2 *= 0.75f;
                float newScale = MathHelper.Lerp(Projectile.scale * 0.75f, 0f, 0.1f * i);
                Main.spriteBatch.Draw(textureGlow, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor2 * 0.5f * Projectile.Opacity, Projectile.oldRot[i], originGlow, newScale, spriteEffects, 0f);
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRectangle, trailColor2 * Projectile.Opacity, Projectile.oldRot[i], origin, Projectile.scale * 1.125f, spriteEffects, 0f);
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, sourceRectangle, trailColor * 0.5f, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, drawColor * 0.5f, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
