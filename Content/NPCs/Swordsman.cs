using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Weapons;

namespace UniverseOfSwords.Content.NPCs
{
    [AutoloadHead]
    public class Swordsman : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 26;
            NPCID.Sets.ExtraFramesCount[Type] = 10;
            NPCID.Sets.DangerDetectRange[Type] = 60;
            NPCID.Sets.AttackFrameCount[Type] = 5;
            NPCID.Sets.AttackTime[Type] = 12;
            NPCID.Sets.AttackAverageChance[Type] = 1;
            NPCID.Sets.AttackType[Type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 10;
            NPC.defense = 30;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }

        public override List<string> SetNPCNameList()
        {
            return base.SetNPCNameList();
        }

        public override string GetChat()
        {
            return base.GetChat();
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "shop";
            }
        }

        public override void AddShops()
        {
            NPCShop shop = new(Type);
            shop.Add(ModContent.ItemType<InnosWrath>(), Condition.DownedCultist)
                .Add(ModContent.ItemType<MasterSword>(), Condition.DownedEyeOfCthulhu)
                .Add(ModContent.ItemType<BarbarianSword>())
                .Register();
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = itemHeight = 32;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 20;
            randExtraCooldown = 8;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = Main.hardMode ? (NPC.downedMoonlord ? 50 : 25) : 20;
            knockback = 5f;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return base.CanTownNPCSpawn(numTownNPCs);
        }


        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            int itemType = ItemID.PlatinumBroadsword;
            if (Main.hardMode && NPC.downedMoonlord)
            {
                itemType = ItemID.Zenith;
            }
            else if (Main.hardMode && NPC.downedGolemBoss)
            {
                itemType = ModContent.ItemType<Golem>();
            }
            else if (!Main.hardMode)
            {
                itemType = ModContent.ItemType<MasterSword>();
            }

            Main.GetItemDrawFrame(itemType, out Texture2D texture, out Rectangle frame);
            item = texture;
            itemFrame = frame;
            itemSize = 32;
        }

        public override bool CanGoToStatue(bool toKingStatue) => toKingStatue;
    }
}
