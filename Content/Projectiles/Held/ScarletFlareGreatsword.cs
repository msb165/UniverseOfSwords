using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    public class ScarletFlareGreatsword : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.ScarletFlareGreatsword>().Texture;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.timeLeft = 120;
            Projectile.extraUpdates = 1;
            Projectile.noEnchantmentVisuals = true;
        }

        Player Player => Main.player[Projectile.owner];
        ref float Timer => ref Projectile.ai[0];
        int delay = 60;

        public override void AI()
        {
            Timer++;
            Projectile.Center = Player.Center;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += MathHelper.Clamp(MathHelper.Lerp(0f, 0.2f * Player.direction, UniverseUtils.Easings.EaseInBack(Timer * 0.025f) * 1.5f), -0.25f, 0.25f);
            if (Projectile.soundDelay == 0)
            {
                SoundEngine.PlaySound(SoundID.Item45 with { Volume = 0.75f }, Projectile.position);
                Projectile.soundDelay = delay > 30 ? delay -= 15 : delay;
            }
            if (!Player.channel || Player.noItems || Player.CCed || Timer > 120f)
            {
                Player.reuseDelay = 2;
            }

            for (int i = 0; i < 10; i++)
            {
                float rotation = Projectile.rotation - (Projectile.direction == -1 ? 0f : MathHelper.PiOver2) - MathHelper.PiOver4;
                Dust dust = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center, Projectile.Center + rotation.ToRotationVector2() * 180f, 0.1f * i), DustID.LifeDrain, Vector2.Zero);
                dust.velocity = (rotation + MathHelper.PiOver4).ToRotationVector2() * Main.rand.NextFloat(0.5f, 1.25f);
                dust.noGravity = true;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.whoAmI != Projectile.whoAmI && Projectile.Colliding(Projectile.Hitbox, proj.Hitbox) && !proj.reflected && !proj.friendly && proj.hostile && Main.rand.Next(1, 100) <= Player.HeldItem.GetGlobalItem<ReflectionChance>().reflectChance)
                    {
                        SoundEngine.PlaySound(SoundID.Item150, Projectile.Center);
                        proj.velocity = -proj.oldVelocity;
                        proj.friendly = true;
                        proj.hostile = false;
                        proj.reflected = true;
                    }
                }
            }

            SetPlayerValues();
        }

        public void SetPlayerValues()
        {
            if (Player.channel)
            {
                Projectile.timeLeft = 16;
            }
            Player.ChangeDir(Projectile.direction);
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            float rotation = Projectile.rotation - MathHelper.Pi - MathHelper.PiOver4;
            if (Player.direction == -1)
            {
                rotation += MathHelper.PiOver4 * 2;
            }
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, rotation);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            float rotation = Projectile.rotation - (Projectile.direction == -1 ? 0f : MathHelper.PiOver2) - MathHelper.PiOver4;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + rotation.ToRotationVector2() * 180f, 4f, ref _);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target) && !Projectile.CanHitWithMeleeWeapon(target))
            {
                return;
            }
            int rand = Main.rand.Next(2);
            if (rand == 0)
            {
                target.AddBuff(BuffID.OnFire, 700); 
            }
            else if (rand == 1)
            {
                Player.Heal(10);
            }

            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 spawnPos = Main.rand.NextVector2CircularEdge(200f, 200f);
                    Vector2 spawnVel = Vector2.Normalize(Player.Center - target.Center - spawnPos);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center + spawnVel * 200f, -spawnVel * 16f, ModContent.ProjectileType<FlareCore>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, ai1: target.whoAmI);
                }
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D texture2 = ModContent.Request<Texture2D>(UniverseUtils.TexturesPath + "SwordSmear").Value;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 drawOrigin = new(Projectile.spriteDirection == 1 ? texture.Width : 0f, texture.Height);
            Vector2 drawOrigin2 = new(Projectile.spriteDirection == 1 ? texture2.Width : 0f, texture2.Height / 2);
            Color drawColor = Color.White * Projectile.Opacity;
            Color trailColor = drawColor with { A = 0 };
            Color trailColor2 = Color.Red with { A = 0 } * Projectile.Opacity;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.91f;
                trailColor2 *= 0.91f;
                Main.spriteBatch.Draw(texture2, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor2 * 0.125f, Projectile.oldRot[i] + MathHelper.PiOver4 * Projectile.direction, drawOrigin2, 0.73f, spriteEffects, 0f);
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor * 0.5f, Projectile.oldRot[i], drawOrigin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
