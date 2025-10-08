using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Consumables;

namespace UniverseOfSwords.Common.GlobalNPCs
{
    public class HitEffect : GlobalNPC
    {
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            bool isMelee = item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed;
            bool canHitTarget = !NPCID.Sets.CountsAsCritter[npc.type] && !npc.immortal && npc.lifeMax > 5 && player.CanHitNPCWithMeleeHit(npc.whoAmI);
            if (isMelee && canHitTarget && player.whoAmI == Main.myPlayer && player.GetModPlayer<UniversePlayer>().meleeCD == 0 && Main.rand.NextBool(4) && Main.hardMode) 
            {
                player.GetModPlayer<UniversePlayer>().meleeCD = 30;
                List<int> bonusList = [ModContent.ItemType<MeleeBonus>(), ModContent.ItemType<MeleeBonus2>(), ItemID.Heart, ModContent.ItemType<PurpleHeart>()];
                if (NPC.downedPlantBoss)
                {
                    bonusList.Add(ModContent.ItemType<YellowHeart>());
                }
                int bonusType = Utils.SelectRandom(Main.rand, bonusList.ToArray());
                int fireBonus = Item.NewItem(item.GetSource_OnHit(npc), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, bonusType);
                Main.item[fireBonus].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                Main.item[fireBonus].velocity.X = Main.rand.Next(10, 31) * 0.2f * hit.HitDirection;
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, fireBonus);
                }
            }
        }
    }
}
