using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Common
{
    public class FlamesBolt : ModProjectile
    {
        public override string Texture => UniverseUtils.TexturesPath + "Empty";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            for (int i = 0; i < 8; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork);
                dust.scale = 1.2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<FlamesBlast>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            Projectile.Resize(150, 150);
            Projectile.Damage();
            for (int i = 0; i < 30; i++)
            {
                Vector2 spawnVel = Vector2.Normalize(Utils.RandomVector2(Main.rand, -10f, 11f)) * Main.rand.Next(6, 12);
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork);
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.position = Projectile.Center + Main.rand.NextVector2Square(-10f, 11f);
                dust.velocity = spawnVel;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadNPC(NPCID.Shimmerfly);
            Texture2D value = TextureAssets.Npc[NPCID.Shimmerfly].Value;
            Rectangle sourceRect = value.Frame(4, 5, 2);
            Vector2 glowOrigin = sourceRect.Size() / 2;
            Color drawColor = Color.Orange with { A = 0 } * Projectile.Opacity;
            Color glowColor = drawColor;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                glowColor *= 0.9f;
                Main.spriteBatch.Draw(value, Projectile.oldPos[i] + Projectile.Size / 2 - Projectile.velocity - Main.screenPosition, sourceRect, glowColor, Projectile.rotation, glowOrigin, Projectile.scale * 1.25f, spriteEffects, 0f);
            }

            return false;
        }
    }
}
