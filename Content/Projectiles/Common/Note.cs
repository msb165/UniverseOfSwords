using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class Note : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public string[] texArray =
            [
                UniverseUtils.VanillaTexturesPath + $"Projectile_{ProjectileID.EighthNote}",
                UniverseUtils.VanillaTexturesPath + $"Projectile_{ProjectileID.QuarterNote}",
                UniverseUtils.VanillaTexturesPath + $"Projectile_{ProjectileID.TiedEighthNote}"
            ];

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(24);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.alpha = 100;
            Projectile.light = 0.3f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.ai[2] * Projectile.direction;
            Projectile.SimpleFadeOut(ai: 0, 30f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GoblinSorcerer, Alpha: 255);
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(texArray[(int)Projectile.ai[1]]);
            Vector2 origin = texture.Size() / 2;
            Color drawColor = Projectile.GetAlpha(lightColor) with { A = 0 };
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
