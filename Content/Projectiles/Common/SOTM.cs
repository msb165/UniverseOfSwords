using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Utilities.Projectiles;

namespace UniverseOfSwordsMod.Content.Projectiles.Common
{
    public class SOTM : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SOTM");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 500;               //The width of projectile hitbox
            Projectile.height = 500;             //The height of projectile hitbox
            Projectile.scale = 1.0F;
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("EmperorBlaze").Type, 1000, true);
        }
    }
}
