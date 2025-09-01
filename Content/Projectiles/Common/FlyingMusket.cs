using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
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

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class FlyingMusket : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Item_{ItemID.Musket}";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            //Projectile.alpha = 255;
            //Projectile.hide = true;
            Projectile.netImportant = true;
        }

        public ref float TargetIndex => ref Projectile.ai[0];
        public ref float Timer => ref Projectile.ai[2];

        public override void AI()
        {
            NPC npc = Main.npc[(int)TargetIndex];
            if (!npc.CanBeChasedBy())
            {
                Projectile.Kill();
                return;
            }
            //Projectile.rotation = Vector2.Normalize(Projectile.Center - Main.npc[(int)TargetIndex].Center).ToRotation() + MathHelper.PiOver2;
            Projectile.direction = MathF.Sign(Main.npc[(int)TargetIndex].Center.X - Projectile.Center.X);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Vector2.Normalize(Projectile.Center - npc.Center).ToRotation() + MathHelper.Pi, 0.18f);
            Timer++;
            if (Main.myPlayer == Projectile.owner && Timer % 30f == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item11, Projectile.position);
                Vector2 spawnVel = Vector2.Normalize(npc.Center - Projectile.Center) * 8f;
                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center + spawnVel, spawnVel, ProjectileID.Bullet, Projectile.damage / 2, 4f, Projectile.owner);
                proj.DamageType = DamageClass.Melee;
            }
        }

        public override bool PreDraw(ref Color lightColor) 
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Vector2 drawOrigin = texture.Size() / 2;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
