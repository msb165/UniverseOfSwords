using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Buffs;
using UniverseOfSwordsMod.Utilities;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class SOTM : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SOTM");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Type] = 15;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Type] = 2;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;               //The width of projectile hitbox
            Projectile.height = 64;             //The height of projectile hitbox
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.light = 1f;
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.SimpleFadeOut(ai: 0, 15f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => target.AddBuff(ModContent.BuffType<EmperorBlaze>(), 1000, true);

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            Vector2 velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(-MathHelper.PiOver2) * Projectile.scale;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center - velocity * 200f, Projectile.Center + velocity * 200f, 48f * Projectile.scale, ref _);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            UniverseUtils.Drawing.DrawWithAfterImages(Projectile, Color.White with { A = 200 } * Projectile.Opacity);
            return false;
        }
    }
}
