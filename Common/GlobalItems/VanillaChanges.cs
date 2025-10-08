using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;
using UniverseOfSwords.Utilities.Projectiles;

namespace UniverseOfSwords.Common.GlobalItems
{
    public class VanillaChanges : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableVanillaChanges)
            {
                switch (entity.type)
                {
                    case ItemID.FetidBaghnakhs:
                    case ItemID.BladedGlove:
                    case ItemID.PsychoKnife:
                    case ItemID.Starfury:
                    case ItemID.PlatinumBroadsword:
                    case ItemID.GoldBroadsword:
                    case ItemID.Meowmere:
                    case ItemID.PearlwoodSword:
                    case ItemID.ChlorophyteSaber:
                        entity.scale = 1.5f;
                        break;
                    case ItemID.ScourgeoftheCorruptor:
                        entity.noUseGraphic = false;
                        entity.noMelee = false;
                        entity.useStyle = ItemUseStyleID.Swing;
                        entity.DamageType = DamageClass.Melee;
                        entity.scale = 1.5f;
                        entity.shootSpeed = 5f;
                        break;
                    case ItemID.VampireKnives:
                        entity.noUseGraphic = false;
                        entity.noMelee = false;
                        entity.useStyle = ItemUseStyleID.Swing;
                        entity.DamageType = DamageClass.Melee;
                        entity.scale = 2f;
                        entity.shootSpeed = 10f;
                        break;
                    case ItemID.EnchantedSword:
                        entity.shootSpeed = 4f;
                        entity.scale = 1.5f;
                        break;
                    case ItemID.InfluxWaver:
                        entity.noMelee = true;
                        entity.shootsEveryUse = true;
                        break;
                }
            }
        }

        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            return;
            if (item.useStyle == ItemUseStyleID.Swing)
            {
                if (item.type > -1 && Item.claw[item.type])
                {
                    if (player.itemAnimation < player.itemAnimationMax * 0.333)
                    {
                        player.itemLocation.X = player.Center.X + (heldItemFrame.Width * 0.5f - 10f) * player.direction;
                        player.itemLocation.Y = player.position.Y + 26f;
                    }
                    else if (player.itemAnimation < player.itemAnimationMax * 0.666)
                    {
                        float offset = 8f;
                        player.itemLocation.X = player.Center.X + (heldItemFrame.Width * 0.5f - offset) * player.direction;
                        offset = 24f;
                        player.itemLocation.Y = player.position.Y + offset;
                    }
                    else
                    {
                        float offset = 6f;
                        player.itemLocation.X = player.Center.X - (heldItemFrame.Width * 0.5f - offset) * player.direction;
                        offset = 20f;
                        player.itemLocation.Y = player.position.Y + offset;
                    }
                    player.itemRotation = (player.itemAnimation / (float)player.itemAnimationMax - 0.5f) * -player.direction * 3.5f - player.direction * 0.3f;
                }
                else
                {
                    float progress = player.itemAnimation / (float)player.itemAnimationMax + 0.5f;
                    item.alpha = (int)MathHelper.Lerp(0, 255, 1f - progress);
                    //Main.NewText(player.itemAnimation / (float)player.itemAnimationMax);
                    if (player.direction == -1)
                    {
                        //progress -= MathHelper.PiOver4;
                    }
                    player.itemRotation = (player.itemAnimation / (float)player.itemAnimationMax - 0.5f) * -player.direction * 3.5f - player.direction * 0.3f;
                    //player.itemRotation *= progress;
                    //player.itemRotation += MathHelper.PiOver4;
                    //if (player.direction == -1)
                    //{
                    //    player.itemRotation -= MathHelper.PiOver2;
                    //}
                    float rotation = player.itemRotation;
                    if (player.direction == -1)
                    {
                        rotation -= MathHelper.PiOver2 + MathHelper.PiOver4;
                    }
                    player.itemLocation = player.Center + rotation.ToRotationVector2() * 8.5f;
                    if (player.direction == 1)
                    {
                        player.itemLocation -= new Vector2(3f * player.direction, 3f);
                    }
                }
                if (player.gravDir == -1f)
                {
                    player.itemRotation = -player.itemRotation;
                    player.itemLocation.Y = player.position.Y + player.height + (player.position.Y - player.itemLocation.Y);
                }
            }
        }


        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableVanillaChanges && UniverseUtils.IsAValidTarget(target))
            {
                switch (item.type)
                {
                    case ItemID.Starfury:
                        Vector2 pointPosition = new(player.Center.X + Main.rand.Next(201) * -player.direction + (target.Center.X - player.position.X), player.MountedCenter.Y - 600f);
                        Vector2 targetPosition = target.Center;
                        Vector2 vec = targetPosition;
                        Vector2 vector60 = (pointPosition - targetPosition).SafeNormalize(new Vector2(0f, -1f));
                        while (vec.Y > pointPosition.Y && WorldGen.SolidTile(vec.ToTileCoordinates()))
                        {
                            vec += vector60 * 16f;
                        }
                        Projectile.NewProjectile(target.GetSource_OnHit(target), pointPosition, -vector60 * item.shootSpeed, ProjectileID.Starfury, damageDone, hit.Knockback, player.whoAmI, 0f, vec.Y);
                        target.AddBuff(BuffID.OnFire, 300);
                        break;
                    case ItemID.OrichalcumSword:
                        int direction = player.direction;
                        Vector2 spawnPos = Main.screenPosition;
                        if (direction < 0)
                        {
                            spawnPos.X += Main.screenWidth;
                        }
                        spawnPos.Y += Main.rand.Next(Main.screenHeight);
                        Vector2 spawnVel = target.Center - spawnPos + Utils.RandomVector2(Main.rand, -50f, 51f) * 0.1f;
                        spawnVel = Vector2.Normalize(spawnVel) * 24f;
                        Projectile.NewProjectileDirect(target.GetSource_OnHit(target), spawnPos, spawnVel, ProjectileID.FlowerPetal, damageDone, 0f, player.whoAmI);
                        break;
                    case ItemID.ChlorophyteSaber:
                        Vector2 newVel = (target.Center - player.Center).SafeNormalize(Vector2.Zero) * 4f;
                        Projectile.NewProjectileDirect(target.GetSource_OnHit(target), player.Center - Vector2.UnitY * 8f + newVel * 3f, newVel.RotatedByRandom(MathHelper.ToRadians(80f)), ModContent.ProjectileType<ChlorophyteBolt>(), damageDone, 0f, player.whoAmI);
                        target.AddBuff(BuffID.Poisoned, 300);
                        break;
                    case ItemID.ChlorophyteClaymore:
                        target.AddBuff(BuffID.Poisoned, 300);
                        break;
                    case ItemID.EnchantedSword:
                    case ItemID.Seedler:
                    case ItemID.Meowmere:
                        Projectile.NewProjectileDirect(target.GetSource_OnHit(target), player.Center, Vector2.Normalize(target.Center - player.Center) * item.shootSpeed, item.shoot, item.damage, item.knockBack, player.whoAmI);
                        break;
                }
            }
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableVanillaChanges)
            {
                switch (item.type)
                {
                    case ItemID.Starfury:
                    case ItemID.ChlorophyteSaber:
                    case ItemID.EnchantedSword:
                    case ItemID.Seedler:
                    case ItemID.Meowmere:
                        return false;
                    case ItemID.InfluxWaver:
                        float adjustedItemScale = player.GetAdjustedItemScale(item);
                        Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ModContent.ProjectileType<InfluxWaverEnergy>(), item.damage, item.knockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
                        NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
                        return false;
                }
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }


    public class ProjectileChanges : GlobalProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults(Projectile entity)
        {
            if (!ModContent.GetInstance<UniverseConfig>().enableVanillaChanges)
            {
                return;
            }
            switch (entity.type)
            {
                case ProjectileID.IceBolt:
                case ProjectileID.FrostBoltSword:
                    entity.timeLeft = 20;
                    break;
                case ProjectileID.EatersBite:
                    entity.aiStyle = -1;
                    break;
                case ProjectileID.DeathSickle:
                    entity.scale = 2f;
                    entity.aiStyle = -1;
                    break;
                case ProjectileID.InfluxWaver:
                    entity.tileCollide = false;
                    break;
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            return base.PreAI(projectile);
        }

        public override bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
        {

            /*if (projectile.type == ProjectileID.MonkStaffT3)
            {
                float rotation = projectile.rotation - MathHelper.PiOver4 * (float)Math.Sign(projectile.velocity.X);
                float _ = 0f;
                float size = 80f;
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + rotation.ToRotationVector2(), projectile.Center + rotation.ToRotationVector2() * size, 23f * projectile.scale, ref _);
            }*/
            return base.Colliding(projectile, projHitbox, targetHitbox);
        }

        public override void AI(Projectile projectile)
        {
            if (!ModContent.GetInstance<UniverseConfig>().enableVanillaChanges)
            {
                return;
            }
            switch (projectile.type)
            {
                case ProjectileID.DeathSickle:
                    projectile.rotation += projectile.direction * 0.5f;
                    projectile.velocity *= 0.96f;
                    projectile.SimpleFadeOut(ai: 0, maxTime: 30f);
                    break;
                case ProjectileID.IceSickle:
                    projectile.SimpleFadeOut(ai: 0, maxTime: 30f);
                    break;
                case ProjectileID.EatersBite:
                    if (projectile.alpha <= 200)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.ScourgeOfTheCorruptor);
                            dust.alpha = projectile.alpha;
                            dust.position = projectile.Center - projectile.velocity / 4f * i;
                            dust.velocity = Vector2.Zero;
                            dust.scale = 0.7f;
                        }
                    }

                    if (projectile.alpha >= 0 && projectile.ai[0] != 1f)
                    {
                        projectile.alpha -= 50;
                        if (projectile.alpha < 0)
                        {
                            projectile.alpha = 0;
                            projectile.ai[0] = 1f;
                        }
                    }
                    if (projectile.ai[0] > 0f)
                    {
                        projectile.SimpleFadeOut(ai: 1, 30f);
                    }
                    projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
                    break;
            }
        }

        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableVanillaChanges)
            {
                switch (projectile.type)
                {
                }
            }
            return base.GetAlpha(projectile, lightColor);
        }

        public override void PostAI(Projectile projectile)
        {

        }

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableVanillaChanges)
            {
                switch (projectile.type)
                {
                    case ProjectileID.DeathSickle:
                    case ProjectileID.IceSickle:
                        UniverseUtils.Drawing.DrawWithAfterImages(projectile, Color.White with { A = 0 } * projectile.Opacity);
                        return false;
                }
            }
            return base.PreDraw(projectile, ref lightColor);
        }
    }
}
