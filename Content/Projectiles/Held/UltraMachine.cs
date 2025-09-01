using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Projectiles.Held
{
    public class UltraMachine : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.UltraMachine>().Texture;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(16);
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 60;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1.125f;
            Projectile.noEnchantmentVisuals = true;
        }

        Player Player => Main.player[Projectile.owner];
        ref float Timer => ref Projectile.ai[0];

        public override void AI()
        {
            Timer++;
            Projectile.Center = Player.Center;
            Projectile.spriteDirection = Projectile.direction;
            float rotation = (Main.MouseWorld - Player.Center).ToRotation();
            Projectile.rotation = rotation + MathHelper.Lerp(-2f, 1f, UniverseUtils.Easings.EaseInSine(Timer * 0.06f));
            SetPlayerValues();
            if (Player.channel)
            {
                Projectile.timeLeft = 30;
            }
            if (Player.noItems || !Player.active)
            {
                Projectile.Kill();
            }
        }

        public void SetPlayerValues()
        {
            Player.ChangeDir(MathF.Sign(Main.MouseWorld.X - Player.Center.X));
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2 - MathHelper.PiOver4);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float _ = 0f;
            float rotation = Projectile.rotation - (Projectile.direction == -1 ? 0f : MathHelper.PiOver2) - MathHelper.PiOver4;
/*            for (int i = 0; i < 10; i++)
            {
                Vector2 spawnPos = Vector2.Lerp(Projectile.Center, Projectile.Center + rotation.ToRotationVector2() * 180 * Projectile.scale, 0.1f * i);
                Dust dust = Dust.NewDustPerfect(spawnPos, DustID.MagicMirror, Vector2.Zero);
                dust.noGravity = true;
            }*/
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + rotation.ToRotationVector2() * 180f * Projectile.scale, 4f, ref _);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), Player.Center + Utils.RandomVector2(Main.rand, -200f, 200f), Vector2.Zero, ModContent.ProjectileType<MachineProbe>(), Projectile.damage, 4f, Projectile.owner);
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = new(0f, texture.Height);
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.None;
            Color trailColor = Color.White with { A = 80 };

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                trailColor *= 0.95f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, null, trailColor * 0.2f, Projectile.oldRot[i], origin, Projectile.scale, spriteEffects, 0f);
            }

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
