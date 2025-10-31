using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons.VanillaAlternatives
{
    internal class ZenithShort : ModItem
    {
        public override string Texture => $"{UniverseUtils.VanillaTexturesPath}Item_{ItemID.Zenith}";
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Zenith);
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = false;
            Item.noUseGraphic = false;
            Item.scale = 1.5f;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(64f * player.direction, -96f), new Vector2(3f * player.direction, 4f));
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int time = (player.itemAnimationMax - player.itemAnimation) / player.itemTime;
            int swordType = FinalFractalHelper.GetRandomProfileIndex();
            if (time == 0)
            {
                swordType = ItemID.Zenith;
            }
            Vector2 targetPos = Main.MouseWorld;
            player.LimitPointToPlayerReachableArea(ref targetPos);
            Vector2 targetVel = targetPos - player.MountedCenter;
            if (time == 1 || time == 2)
            {
                bool zenithTarget = GetZenithTarget(targetPos, 300f, out int npcTargetIndex);
                if (zenithTarget)
                {
                    targetVel = Main.npc[npcTargetIndex].Center - player.MountedCenter;
                }
                bool flag = time == 2;
                if (time == 1 && !zenithTarget)
                {
                    flag = true;
                }
                if (flag)
                {
                    targetVel += Main.rand.NextVector2Circular(150f, 150f);
                }
            }
            Vector2 newVel = Vector2.Normalize(targetVel) * 100f;
            Projectile.NewProjectile(source, position, newVel, type, damage, knockback, player.whoAmI, Main.rand.Next(-50, 51), swordType);
            return false;
        }

        private bool GetZenithTarget(Vector2 searchCenter, float maxDistance, out int npcTargetIndex)
        {
            npcTargetIndex = 0;
            int? index = null;
            float distance = maxDistance;
            foreach (NPC nPC in Main.ActiveNPCs)
            {
                if (nPC.CanBeChasedBy(this))
                {
                    float num3 = searchCenter.Distance(nPC.Center);
                    if (!(distance <= num3))
                    {
                        index = nPC.whoAmI;
                        distance = num3;
                    }
                }
            }
            if (!index.HasValue)
            {
                return false;
            }
            npcTargetIndex = index.Value;
            return true;
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff(Utils.SelectRandom(Main.rand, BuffID.CursedInferno, BuffID.Venom, BuffID.Ichor), 300);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Zenith)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
