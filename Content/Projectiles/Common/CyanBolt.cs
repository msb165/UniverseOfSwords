using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class CyanBolt : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(6);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.noEnchantmentVisuals = true;
        }

        Player Player => Main.player[Projectile.owner];

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[1] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item88, Projectile.position);
                Projectile.ai[1] = 1f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[0] % 72f == 0f && Main.myPlayer == Projectile.owner)
            {
                Vector2 newVel = Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.PiOver2 * Projectile.direction);
                Vector2 newVel2 = Vector2.Normalize(Projectile.velocity).RotatedBy(-MathHelper.PiOver2 * Projectile.direction);
                Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, (newVel * 8f).RotatedBy(Projectile.rotation), Player.beeType(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, (newVel2 * 8f).RotatedBy(Projectile.rotation), Player.beeType(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            for (int j = 0; j < 3; j++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Cyan, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                dust.velocity *= 0.3f;
                dust.position = Projectile.Center - Projectile.velocity / 3f * j;
                dust.scale = 0.9f;
                dust.noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Zombie103, Projectile.position);
            Projectile.Resize(100, 100);
            for (int j = 0; j < 30; j++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Cyan, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, Scale: 1.5f);
                dust.velocity *= 3f;
                dust.noGravity = true;
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Cyan, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, Scale: 2f);
                dust2.velocity *= 2f;
                dust2.noGravity = true;
            }
            Projectile.Damage();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 origin = texture.Size() / 2;
            Color drawColor = new Color(90, 200, 240) with { A = 40 };
            Color trailColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.9f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
