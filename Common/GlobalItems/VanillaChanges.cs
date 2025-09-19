using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Common.GlobalItems
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
                    case ItemID.PsychoKnife:
                    case ItemID.PearlwoodSword:
                    case ItemID.ChlorophyteSaber:
                        entity.scale = 1.25f;
                        break;
                }
            }
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableVanillaChanges)
            {
                switch (item.type)
                {
                    case ItemID.OrichalcumSword:
                        if (UniverseUtils.IsAValidTarget(target))
                        {
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
                        }
                        break;
                    case ItemID.ChlorophyteSaber:
                    case ItemID.ChlorophyteClaymore:
                        target.AddBuff(BuffID.Poisoned, 300);
                        break;
                }
            }
        }


        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableVanillaChanges)
            {
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
