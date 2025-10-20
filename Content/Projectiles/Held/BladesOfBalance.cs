using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held
{
    internal class BladesOfBalance : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.BladesOfBalance>().Texture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(8);
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.noEnchantmentVisuals = true;
            Projectile.extraUpdates = 1;
        }

        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = Projectile.velocity.ToRotation();
            }
            float direction = (MathF.Cos(Projectile.localAI[0]) >= 0f).ToDirectionInt();
            if (Projectile.ai[1] <= 0f)
            {
                direction *= -1f;
            }
            Projectile.ai[0]++;
            Vector2 spinningpoint = (direction * (Projectile.ai[0] / 30f * MathHelper.TwoPi - MathHelper.PiOver2)).ToRotationVector2();
            spinningpoint.Y *= MathF.Sin(Projectile.ai[1]) * 2f;
            if (Projectile.ai[1] <= 0f)
            {
                spinningpoint.Y *= -1f;
            }
            spinningpoint = spinningpoint.RotatedBy(Projectile.localAI[0]);
            if (Projectile.ai[0] < 30f)
            {
                Projectile.velocity += 3f * spinningpoint;
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            Projectile.Center = Player.RotatedRelativePoint(Player.MountedCenter, addGfxOffY: false) + Vector2.Normalize(Projectile.velocity) + Vector2.Normalize(Projectile.rotation.ToRotationVector2());

            Projectile.scale = 1.5f + spinningpoint.X;

            if (Main.myPlayer == Projectile.owner)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.whoAmI != Projectile.whoAmI && Projectile.Colliding(Projectile.Hitbox, proj.Hitbox) && !proj.reflected && proj.hostile && Main.rand.Next(1, 100) <= Player.HeldItem.GetGlobalItem<ReflectionChance>().reflectChance)
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Damage();
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target) && Main.myPlayer == Projectile.owner)
            {
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
                Vector2 positionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
                ParticleOrchestraSettings orchestraSettings = default;
                orchestraSettings.PositionInWorld = positionInWorld;
                ParticleOrchestraSettings settings = orchestraSettings;
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge, settings, Projectile.owner);
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TrueExcalibur, settings, Projectile.owner);
            }
        }

        public void SetPlayerValues()
        {
            //Player.ChangeDir(Projectile.direction);
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            float rotation = Projectile.rotation - MathHelper.PiOver4;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Player.Center, Player.Center + rotation.ToRotationVector2() * 92f * Projectile.scale, 4f, ref _);
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = new(0f, texture.Height);
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.None;

            Main.spriteBatch.Draw(texture, Player.Center - Main.screenPosition, null, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
