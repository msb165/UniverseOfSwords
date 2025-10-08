using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Weapons;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class FlyingSword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(32);
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
        }

        Player Player => Main.player[Projectile.owner];
        public ref float Timer => ref Projectile.ai[0];
        public override void AI()
        {
            Timer += 0.1f;
            Projectile.rotation = (Player.Center - Projectile.Center).ToRotation() - MathHelper.PiOver4;
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.timeLeft = 2;
                Projectile.Center = Player.Center - Timer.ToRotationVector2() * 120f;
            }
            if (!Player.active || Player.HeldItem.type != ModContent.ItemType<BigCrunch>())
            {
                Projectile.Kill();
            }
            Lighting.AddLight(Projectile.Center, Color.Green.ToVector3());
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.Clentaminator_Green, Projectile.velocity * 0.1f, 100, Scale: 1.5f);
                dust.scale = 1.5f;
                dust.position = Projectile.Center - Projectile.velocity.ToRotation().ToRotationVector2();
                dust.rotation = Projectile.velocity.ToRotation();
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D textureGlow = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 origin = texture.Size() / 2;
            Vector2 originGlow = textureGlow.Size() / 2;
            Color drawColor = Color.Green with { A = 0 };
            Color trailColor = drawColor;
            Color trailColor2 = Color.GreenYellow with { A = 0 };
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.6f;
                trailColor2 *= 0.6f;
                Main.spriteBatch.Draw(textureGlow, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor2 * 0.5f, Projectile.oldRot[i] + MathHelper.PiOver4, originGlow, Projectile.scale, spriteEffects, 0f);
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(textureGlow, Projectile.Center - Main.screenPosition, null, drawColor * 0.25f, Projectile.rotation + MathHelper.PiOver4, originGlow, Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
