using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using UniverseOfSwords.Content.Items.Consumables;
using UniverseOfSwords.Content.Items.Materials;
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

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Like)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Like);
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
            return
            [
                "John",
                "Brook",
            ];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();
            chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue1"));
            if (NPC.downedBoss3)
            {
                chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue2"));
            }
            if (DD2Event.DownedInvasionT3 && NPC.downedGolemBoss && NPC.downedMartians)
            {
                chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue3"));
            }
            else if (Main.hardMode)
            {
                chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue4"));
            }
            chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue5"));
            if (NPC.downedMoonlord && !Main.LocalPlayer.HasItem(ModContent.ItemType<SwordOfTheMultiverse>()))
            {
                chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue6"));
            }
            else if (NPC.downedMoonlord)
            {
                chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue7"));
            }
            chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue8"));
            chat.Add(Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue9"));
            string newChat = (string)chat;
            if (newChat == Language.GetTextValue("Mods.UniverseOfSwords.NPCs.Swordsman.Dialogue5"))
            {
                Main.npcChatCornerItem = ModContent.ItemType<UpgradeMatter>();
            }
            return newChat;
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }
        }

        public override void AddShops()
        {
            NPCShop shop = new(Type);
            shop.Add(ModContent.ItemType<InnosWrath>(), Condition.DownedCultist)
                .Add(ModContent.ItemType<MasterSword>(), Condition.DownedEyeOfCthulhu)
                .Add(ModContent.ItemType<NordMead>(), Condition.DownedSkeletron)
                .Add(ModContent.ItemType<MagicSword>(), Condition.DownedSkeletron)
                .Add(ModContent.ItemType<VugarMutater>(), Condition.DownedPlantera)
                .Add(ModContent.ItemType<BarbarianSword>())
                .Add(ModContent.ItemType<CalculatorSword>())
                .Add(ModContent.ItemType<TrueTerrablade>(), Condition.DownedGolem, Condition.DownedMartians, new("Mods.UniverseOfSwords.Conditions.DownedInvasionT3", () => DD2Event.DownedInvasionT3))
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

        public override bool CanTownNPCSpawn(int numTownNPCs) => NPC.downedBoss2;


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
