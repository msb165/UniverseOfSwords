using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class GnomSword : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.SkyFracture}";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 60;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
            Projectile.noEnchantmentVisuals = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 17;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.frame = (int)Projectile.ai[1];
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle sourceRect = new(38 * Projectile.frame, 0, 38, 38);
            Vector2 origin = sourceRect.Size() / 2;
            Color drawColor = Color.Lerp(new Color(80, 120, 255), new Color(255, 80, 80), UniverseUtils.Easings.EaseInSine((float)Main.timeForVisualEffects / 20f)) with { A = 20 } * Projectile.Opacity;
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.75f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition - Projectile.velocity / Projectile.oldPos.Length * i, sourceRect, trailColor with { A = 0 } * 0.5f, Projectile.rotation, origin, Projectile.scale * 1.5f, spriteEffects, 0f);
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition - Projectile.velocity / Projectile.oldPos.Length * i, sourceRect, trailColor with { A = 0 } * 0.5f, Projectile.rotation, origin, Projectile.scale * 2f, spriteEffects, 0f);
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition - Projectile.velocity / Projectile.oldPos.Length * i, sourceRect, trailColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRect, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
