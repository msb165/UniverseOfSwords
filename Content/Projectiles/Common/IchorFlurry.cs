using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    internal class IchorFlurry : ModProjectile
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Projectile_{ProjectileID.Arkhalis}";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 28;
        }

        public override void SetDefaults()
        {
            Projectile.width = 68;
            Projectile.height = 64;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.alpha = 100;
            Projectile.scale = 1.25f;
        }

        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
                Projectile.soundDelay = 6;
            }
            Projectile.spriteDirection = Projectile.direction;
            Projectile.velocity = Vector2.Normalize(Main.MouseWorld - Player.Center);
            Projectile.Center = Player.RotatedRelativePoint(Player.MountedCenter) + Projectile.velocity * 24f * Projectile.scale;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (!Player.channel)
            {
                Projectile.Kill();
            }
            SetPlayerValues();
            FindFrame();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 300);
        }

        public void FindFrame()
        {
            if ((++Projectile.frame) >= Main.projFrames[Type])
            {
                Projectile.frame = 0;
            }
        }

        public void SetPlayerValues()
        {
            Player.ChangeDir(Projectile.direction);
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            Player.itemRotation = MathHelper.WrapAngle((Projectile.velocity * Projectile.direction).ToRotation());
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;
            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2;
            Color drawColor = Color.Yellow with { A = 0 } * Projectile.Opacity;

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
