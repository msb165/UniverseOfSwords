using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;

namespace UniverseOfSwordsMod.Utilities
{
    public partial class UniverseUtils
    {
        public struct Spawn
        {
            public static void SummonGenericSlash(Vector2 target, Color drawColor, int owner, int damage, int drawAlpha = 255, float lerpToWhite = 0f)
            {
                System.Drawing.Color newColor = System.Drawing.Color.FromArgb(drawColor.R, drawColor.G, drawColor.B);
                float colorHue = newColor.GetHue() / 360f;

                Vector2 v = Main.rand.NextVector2CircularEdge(200f, 200f);
                if (v.Y < 0f)
                {
                    v.Y *= -1f;
                }
                v.Y += 100f;
                Vector2 vector = v.SafeNormalize(Vector2.UnitY) * 6f;
                Projectile.NewProjectile(Projectile.GetSource_None(), target - vector * 20f, vector, ModContent.ProjectileType<GenericSlash>(), damage, 0f, owner, lerpToWhite, ai1: colorHue, ai2: drawAlpha);
            }

            public static void VampireHeal(int dmg, Vector2 Position, Entity victim, Player owner)
            {
                float healAmt = dmg * 0.075f;
                if ((int)healAmt != 0 && !(Main.LocalPlayer.lifeSteal <= 0f))
                {
                    Main.LocalPlayer.lifeSteal -= healAmt;
                    int plrIndex = owner.whoAmI;
                    Projectile.NewProjectileDirect(victim.GetSource_OnHit(victim), Position, Vector2.Zero, ProjectileID.VampireHeal, 0, 0f, plrIndex, plrIndex, healAmt);
                }
            }
        }

        public struct Drawing
        {
            public static void DrawWithAfterImages(Projectile proj, Color drawColor)
            {
                Texture2D texture = TextureAssets.Projectile[proj.type].Value;
                Vector2 origin = texture.Size() / 2;
                Color trailColor = drawColor;
                SpriteEffects spriteEffects = proj.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                for (int i = 0; i < proj.oldPos.Length; i++)
                {
                    trailColor *= 0.75f;
                    Main.spriteBatch.Draw(texture, proj.oldPos[i] + proj.Size / 2 - Main.screenPosition, null, trailColor, proj.oldRot[i], origin, proj.scale, spriteEffects, 0f);
                }

                Main.spriteBatch.Draw(texture, proj.Center - Main.screenPosition, null, drawColor, proj.rotation, origin, proj.scale, spriteEffects, 0f);
            }

            public static void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
            {
                Texture2D tex = TextureAssets.Extra[ExtrasID.SharpTears].Value;
                Color shine = shineColor * opacity * 0.5f;
                shine.A = 0;
                Vector2 origin = tex.Size() / 2f;
                Color halfColor = drawColor * 0.5f;
                float lerpValue = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
                Vector2 starScale = new Vector2(fatness.X * 0.5f, scale.X) * lerpValue;
                Vector2 starScale2 = new Vector2(fatness.Y * 0.5f, scale.Y) * lerpValue;
                shine *= lerpValue;
                halfColor *= lerpValue;
                Main.EntitySpriteDraw(tex, drawpos, null, shine, MathHelper.PiOver2 + rotation, origin, starScale, dir);
                Main.EntitySpriteDraw(tex, drawpos, null, shine, rotation, origin, starScale2, dir);
                Main.EntitySpriteDraw(tex, drawpos, null, halfColor, MathHelper.PiOver2 + rotation, origin, starScale * 0.6f, dir);
                Main.EntitySpriteDraw(tex, drawpos, null, halfColor, rotation, origin, starScale2 * 0.6f, dir);
            }
        }
    }
}
