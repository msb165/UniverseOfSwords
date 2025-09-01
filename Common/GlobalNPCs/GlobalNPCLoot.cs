using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Items.Accessories;
using UniverseOfSwordsMod.Content.Items.Consumables;
using UniverseOfSwordsMod.Content.Items.Materials;
using UniverseOfSwordsMod.Content.Items.Weapons;

namespace UniverseOfSwordsMod.Common.GlobalNPCs
{
    public class UniverseOfSwordsModGlobalNPC : GlobalNPC
    {

        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            if (Main.rand.NextBool(2))
            {
                shop[nextSlot] = ModContent.ItemType<Skooma>();
                nextSlot++;
            }
            if (Main.rand.NextBool(5))
            {
                shop[nextSlot] = ModContent.ItemType<PianoSword1>();
                nextSlot++;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            Conditions.IsHardmode hardMode = new();
            Conditions.IsPreHardmode notHardMode = new();
            Conditions.DownedPlantera downedPlantera = new();
            Conditions.IsExpert isExpert = new();            

            if (npc.lifeMax > 5 && !npc.immortal && !npc.boss && !NPCID.Sets.CountsAsCritter[npc.type])
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordMatter>(), 20, 1, 12));
            }

            if (npc.boss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(hardMode, ModContent.ItemType<UpgradeMatter>(), 10, 1, 6));
                npcLoot.Add(ItemDropRule.ByCondition(downedPlantera, ModContent.ItemType<SwordShard>(), 1, 1, 4));
            }

            if (Array.IndexOf([NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail], npc.type) > -1)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.LegacyHack_IsABoss(), ModContent.ItemType<TheEater>()));
            }

            switch (npc.type)
            {
                case NPCID.EyeofCthulhu:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<CthulhuJudge>()));
                    break;
                case NPCID.KingSlime:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<StickyGlowstickSword>()));
                    break;
                case NPCID.BrainofCthulhu:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<TheBrain>()));
                    break;
                case NPCID.SkeletronHead:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<SwordOfPower>()));
                    break;
                case NPCID.SkeletronPrime:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<PrimeSword>()));
                    break;
                case NPCID.Spazmatism:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<TwinsSword>()));
                    break;
                case NPCID.TheDestroyer:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<DestroyerSword>()));
                    break;
                case NPCID.Golem:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<Golem>()));
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<SolBlade>(), 100));
                    break;
                case NPCID.CultistBoss:
                    npcLoot.Add(ItemDropRule.ByCondition(isExpert, ModContent.ItemType<Doomsday>()));
                    break;
                case NPCID.Paladin:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PaladinSword>()));
                    break;
                case NPCID.DungeonGuardian:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HaloOfHorrors>()));
                    break;
                case NPCID.RedDevil:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScarletFlareCore>()));
                    break;
                case NPCID.GreekSkeleton:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CaesarSword>()));
                    break;
                case NPCID.BlackRecluse:
                case NPCID.BlackRecluseWall:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PoisonSword>()));
                    break;
                case NPCID.Vampire:
                case NPCID.VampireBat:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DraculaSword>()));
                    break;
                case NPCID.LunarTowerNebula:
                case NPCID.LunarTowerSolar:
                case NPCID.LunarTowerVortex:
                case NPCID.LunarTowerStardust:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BeliarClaw>()));
                    break;
            }
        }

        /*public override void OnKill(NPC npc)
        {
            if (npc.lifeMax > 5 && npc.value > 0f)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordMatter"));
            }
            if (npc.type == NPCID.EyeofCthulhu && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("CthulhuJudge"));
            }
            if (npc.type == NPCID.KingSlime && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("StickyGlowstickSword"));
            }
            if (npc.type == NPCID.EaterofWorldsTail && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("TheEater"));
            }
            if (npc.type == NPCID.BrainofCthulhu && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("TheBrain"));
            }
            if (npc.type == NPCID.SkeletronHead && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfPower"));
            }
            if (npc.type == NPCID.SkeletronPrime && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PrimeSword"));
            }
            if (npc.type == NPCID.Spazmatism && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("TwinsSword"));
            }
            if (npc.type == NPCID.TheDestroyer && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DestroyerSword"));
            }
            if (npc.type == NPCID.Plantera && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Executioner"));
            }
            if (npc.type == NPCID.Golem && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Golem"));
            }
            if (npc.type == NPCID.CultistBoss && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Doomsday"));
            }
            if (npc.type == NPCID.CultistBoss && Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Doomsday"));
            }
            if (npc.type == NPCID.DukeFishron && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Sharkron"));
            }
            if (npc.type == NPCID.Paladin && !Main.expertMode)
            {
                if (Main.rand.Next(0, 7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PaladinSword"));
                }
            }
            if (npc.type == NPCID.Paladin && Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PaladinSword"));
                }
            }
            if (npc.type == NPCID.Vampire && !Main.expertMode)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DraculaSword"));
                }
            }
            if (npc.type == NPCID.Vampire && Main.expertMode)
            {
                if (Main.rand.Next(40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DraculaSword"));
                }
            }
            if (npc.type == NPCID.VampireBat && !Main.expertMode)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DraculaSword"));
                }
            }
            if (npc.type == NPCID.VampireBat && Main.expertMode)
            {
                if (Main.rand.Next(45) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DraculaSword"));
                }
            }
            if (npc.type == NPCID.MartianSaucerCore && !Main.expertMode)
            {
                if (Main.rand.Next(0, 2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MartianSaucerCore"));
                }
            }
            if (npc.type == NPCID.MartianSaucerCore && Main.expertMode)
            {
                if (Main.rand.Next(0, 1) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MartianSaucerCore"));
                }
            }
            if (npc.type == NPCID.Frankenstein && !Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("FingerofDoom"));
                }
            }
            if (npc.type == NPCID.Frankenstein && Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("FingerofDoom"));
                }
            }
            if (npc.type == NPCID.Unicorn && !Main.expertMode)
            {
                if (Main.rand.Next(0, 15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("GiantUnicornHorn"));
                }
            }
            if (npc.type == NPCID.Unicorn && Main.expertMode)
            {
                if (Main.rand.Next(0, 10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("GiantUnicornHorn"));
                }
            }
            if (npc.type == NPCID.GreekSkeleton && !Main.expertMode)
            {
                if (Main.rand.Next(0, 15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("CaesarSword"));
                }
            }
            if (npc.type == NPCID.GreekSkeleton && Main.expertMode)
            {
                if (Main.rand.Next(0, 11) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("CaesarSword"));
                }
            }
            if (npc.type == NPCID.PirateShip && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DutchSword"));
                }
            }
            if (npc.type == NPCID.PirateShip && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DutchSword"));
                }
            }
            if (npc.type == NPCID.BigMimicHallow && !Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ShardSword"));
                }
            }
            if (npc.type == NPCID.BigMimicHallow && Main.expertMode)
            {
                if (Main.rand.Next(0, 2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ShardSword"));
                }
            }
            if (npc.type == NPCID.BigMimicCrimson && !Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DartSword"));
                }
            }
            if (npc.type == NPCID.BigMimicCrimson && Main.expertMode)
            {
                if (Main.rand.Next(0, 2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DartSword"));
                }
            }
            if (npc.type == NPCID.BigMimicCorruption && !Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ClingerSword"));
                }
            }
            if (npc.type == NPCID.BigMimicCorruption && Main.expertMode)
            {
                if (Main.rand.Next(0, 2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ClingerSword"));
                }
            }
            if (npc.type == NPCID.BigMimicJungle && !Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RottenSword"));
                }
            }
            if (npc.type == NPCID.BigMimicJungle && Main.expertMode)
            {
                if (Main.rand.Next(0, 2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RottenSword"));
                }
            }
            if (npc.type == NPCID.RedDevil && !Main.expertMode)
            {
                if (Main.rand.Next(0, 15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DevilSword"));
                }
            }
            if (npc.type == NPCID.RedDevil && Main.expertMode)
            {
                if (Main.rand.Next(0, 13) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DevilSword"));
                }
            }
            if (npc.type == NPCID.Demon && !Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DeathSword"));
                }
            }
            if (npc.type == NPCID.Demon && Main.expertMode)
            {
                if (Main.rand.Next(0, 35) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DeathSword"));
                }
            }
            if (npc.type == NPCID.GoblinWarrior && !Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Sting"));
                }
            }
            if (npc.type == NPCID.GoblinWarrior && Main.expertMode)
            {
                if (Main.rand.Next(0, 25) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Sting"));
                }
            }
            if (npc.type == NPCID.LunarTowerVortex && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("InnosWrath"));
                }
            }
            if (npc.type == NPCID.LunarTowerVortex && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("InnosWrath"));
                }
            }
            if (npc.type == NPCID.LunarTowerNebula && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("InnosWrath"));
                }
            }
            if (npc.type == NPCID.LunarTowerNebula && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("InnosWrath"));
                }
            }
            if (npc.type == NPCID.LunarTowerStardust && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("InnosWrath"));
                }
            }
            if (npc.type == NPCID.LunarTowerStardust && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("InnosWrath"));
                }
            }
            if (npc.type == NPCID.LunarTowerSolar && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("InnosWrath"));
                }
            }
            if (npc.type == NPCID.LunarTowerSolar && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("InnosWrath"));
                }
            }
            if (npc.type == NPCID.LunarTowerVortex && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BeliarClaw"));
                }
            }
            if (npc.type == NPCID.LunarTowerVortex && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BeliarClaw"));
                }
            }
            if (npc.type == NPCID.LunarTowerNebula && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BeliarClaw"));
                }
            }
            if (npc.type == NPCID.LunarTowerNebula && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BeliarClaw"));
                }
            }
            if (npc.type == NPCID.LunarTowerStardust && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BeliarClaw"));
                }
            }
            if (npc.type == NPCID.LunarTowerStardust && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BeliarClaw"));
                }
            }
            if (npc.type == NPCID.LunarTowerSolar && !Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BeliarClaw"));
                }
            }
            if (npc.type == NPCID.LunarTowerSolar && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BeliarClaw"));
                }
            }
            if (npc.type == NPCID.GoblinPeon && !Main.expertMode)
            {
                if (Main.rand.Next(0, 20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("GoblinKnife"));
                }
            }
            if (npc.type == NPCID.GoblinPeon && Main.expertMode)
            {
                if (Main.rand.Next(0, 17) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("GoblinKnife"));
                }
            }
            if (npc.type == NPCID.FireImp && !Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Fireball"));
                }
            }
            if (npc.type == NPCID.FireImp && Main.expertMode)
            {
                if (Main.rand.Next(0, 25) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Fireball"));
                }
            }
            if (npc.type == NPCID.GiantBat && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BatSlayer"));
                }
            }
            if (npc.type == NPCID.GiantBat && Main.expertMode)
            {
                if (Main.rand.Next(0, 45) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("BatSlayer"));
                }
            }
            if (npc.type == NPCID.Piranha && !Main.expertMode)
            {
                if (Main.rand.Next(0, 80) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Biter"));
                }
            }
            if (npc.type == NPCID.Piranha && Main.expertMode)
            {
                if (Main.rand.Next(0, 70) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Biter"));
                }
            }
            if (npc.type == NPCID.DungeonSlime && !Main.expertMode)
            {
                if (Main.rand.Next(0, 10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SlimeKiller"));
                }
            }
            if (npc.type == NPCID.DungeonSlime && Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SlimeKiller"));
                }
            }
            if (npc.type == NPCID.TheGroom && !Main.expertMode)
            {
                if (Main.rand.Next(0, 1) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("UselessWeapon"));
                }
            }
            if (npc.type == NPCID.TheGroom && Main.expertMode)
            {
                if (Main.rand.Next(0, 1) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("UselessWeapon"));
                }
            }
            if (npc.type == NPCID.Werewolf && !Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WolfDestroyer"));
                }
            }
            if (npc.type == NPCID.Werewolf && Main.expertMode)
            {
                if (Main.rand.Next(0, 25) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WolfDestroyer"));
                }
            }
            if (npc.type == NPCID.Wraith && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WraithBlade"));
                }
            }
            if (npc.type == NPCID.Wraith && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WraithBlade"));
                }
            }
            if (npc.type == NPCID.Zombie && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.Zombie && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.ArmedZombie && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.ArmedZombie && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.PossessedArmor && !Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PossessedSword"));
                }
            }
            if (npc.type == NPCID.PossessedArmor && Main.expertMode)
            {
                if (Main.rand.Next(0, 35) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PossessedSword"));
                }
            }
            if (npc.type == NPCID.BaldZombie && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.BaldZombie && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.PincushionZombie && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.PincushionZombie && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.SlimedZombie && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.SlimedZombie && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.SwampZombie && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.SwampZombie && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.TwiggyZombie && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.TwiggyZombie && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.FemaleZombie && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.FemaleZombie && Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ZombieKnife"));
                }
            }
            if (npc.type == NPCID.Mimic && !Main.expertMode)
            {
                if (Main.rand.Next(0, 4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ElBastardo"));
                }
            }
            if (npc.type == NPCID.Mimic && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ElBastardo"));
                }
            }
            if (npc.type == NPCID.GiantCursedSkull && !Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WeirdSword"));
                }
            }
            if (npc.type == NPCID.GiantCursedSkull && Main.expertMode)
            {
                if (Main.rand.Next(0, 20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WeirdSword"));
                }
            }
            if (npc.type == NPCID.DarkCaster && !Main.expertMode)
            {
                if (Main.rand.Next(0, 40) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WaterBoltSword"));
                }
            }
            if (npc.type == NPCID.DarkCaster && Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WaterBoltSword"));
                }
            }
            if (npc.type == NPCID.Harpy && !Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("FeatherDuster"));
                }
            }
            if (npc.type == NPCID.Harpy && Main.expertMode)
            {
                if (Main.rand.Next(0, 20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("FeatherDuster"));
                }
            }
            if (npc.type == NPCID.WyvernHead && !Main.expertMode)
            {
                if (Main.rand.Next(0, 10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SkyPower"));
                }
            }
            if (npc.type == NPCID.WyvernHead && Main.expertMode)
            {
                if (Main.rand.Next(0, 5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SkyPower"));
                }
            }
            if (npc.type == NPCID.RustyArmoredBonesAxe && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RustySword"));
                }
            }
            if (npc.type == NPCID.RustyArmoredBonesAxe && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RustySword"));
                }
            }
            if (npc.type == NPCID.RustyArmoredBonesFlail && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RustySword"));
                }
            }
            if (npc.type == NPCID.RustyArmoredBonesFlail && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RustySword"));
                }
            }
            if (npc.type == NPCID.RustyArmoredBonesSword && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RustySword"));
                }
            }
            if (npc.type == NPCID.RustyArmoredBonesSword && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RustySword"));
                }
            }
            if (npc.type == NPCID.RustyArmoredBonesSwordNoArmor && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RustySword"));
                }
            }
            if (npc.type == NPCID.RustyArmoredBonesSwordNoArmor && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("RustySword"));
                }
            }
            if (npc.type == NPCID.BlueArmoredBones && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MagnetSword"));
                }
            }
            if (npc.type == NPCID.BlueArmoredBones && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MagnetSword"));
                }
            }
            if (npc.type == NPCID.BlueArmoredBonesMace && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MagnetSword"));
                }
            }
            if (npc.type == NPCID.BlueArmoredBonesMace && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MagnetSword"));
                }
            }
            if (npc.type == NPCID.BlueArmoredBonesNoPants && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MagnetSword"));
                }
            }
            if (npc.type == NPCID.BlueArmoredBonesNoPants && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MagnetSword"));
                }
            }
            if (npc.type == NPCID.BlueArmoredBonesSword && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MagnetSword"));
                }
            }
            if (npc.type == NPCID.BlueArmoredBonesSword && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("MagnetSword"));
                }
            }
            if (npc.type == NPCID.HellArmoredBones && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfFlames"));
                }
            }
            if (npc.type == NPCID.HellArmoredBones && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfFlames"));
                }
            }
            if (npc.type == NPCID.HellArmoredBonesMace && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfFlames"));
                }
            }
            if (npc.type == NPCID.HellArmoredBonesMace && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfFlames"));
                }
            }
            if (npc.type == NPCID.HellArmoredBonesSword && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfFlames"));
                }
            }
            if (npc.type == NPCID.HellArmoredBonesSword && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfFlames"));
                }
            }
            if (npc.type == NPCID.HellArmoredBonesSpikeShield && !Main.expertMode)
            {
                if (Main.rand.Next(0, 150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfFlames"));
                }
            }
            if (npc.type == NPCID.HellArmoredBonesSpikeShield && Main.expertMode)
            {
                if (Main.rand.Next(0, 120) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SwordOfFlames"));
                }
            }
            if (npc.type == NPCID.MossHornet && !Main.expertMode)
            {
                if (Main.rand.Next(0, 1250) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DragonsDeath"));
                }
            }
            if (npc.type == NPCID.MossHornet && Main.expertMode)
            {
                if (Main.rand.Next(0, 1050) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DragonsDeath"));
                }
            }
            if (npc.type == NPCID.Arapaima && !Main.expertMode)
            {
                if (Main.rand.Next(0, 1250) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DragonsDeath"));
                }
            }
            if (npc.type == NPCID.Arapaima && Main.expertMode)
            {
                if (Main.rand.Next(0, 1050) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DragonsDeath"));
                }
            }
            if (npc.type == NPCID.FlyingSnake && !Main.expertMode)
            {
                if (Main.rand.Next(0, 1000) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DragonsDeath"));
                }
            }
            if (npc.type == NPCID.FlyingSnake && Main.expertMode)
            {
                if (Main.rand.Next(0, 900) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DragonsDeath"));
                }
            }
            if (npc.type == NPCID.Crab && !Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("OceanRoar"));
                }
            }
            if (npc.type == NPCID.Crab && Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("OceanRoar"));
                }
            }
            if (npc.type == NPCID.BlackRecluse && !Main.expertMode)
            {
                if (Main.rand.Next(0, 70) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PoisonSword"));
                }
            }
            if (npc.type == NPCID.BlackRecluse && Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PoisonSword"));
                }
            }
            if (npc.type == NPCID.BlackRecluseWall && !Main.expertMode)
            {
                if (Main.rand.Next(0, 70) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PoisonSword"));
                }
            }
            if (npc.type == NPCID.BlackRecluseWall && Main.expertMode)
            {
                if (Main.rand.Next(0, 50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PoisonSword"));
                }
            }
            if (npc.type == NPCID.GoblinSummoner && !Main.expertMode)
            {
                if (Main.rand.Next(0, 6) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PhantomScimitar"));
                }
            }
            if (npc.type == NPCID.GoblinSummoner && Main.expertMode)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("PhantomScimitar"));
                }
            }
            if (npc.type == NPCID.GraniteGolem && !Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WitherBane"));
                }
            }
            if (npc.type == NPCID.GraniteGolem && Main.expertMode)
            {
                if (Main.rand.Next(0, 15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("WitherBane"));
                }
            }

            if (npc.type == NPCID.DungeonGuardian && !Main.expertMode)
            {
                if (Main.rand.Next(0, 100) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("HaloOfHorrors"));
                }
            }
            if (npc.type == NPCID.DungeonGuardian && Main.expertMode)
            {
                if (Main.rand.Next(0, 100) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("HaloOfHorrors"));
                }
            }

            if (npc.type == NPCID.DrManFly && !Main.expertMode)
            {
                if (Main.rand.Next(0, 30) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("HeisenbergsFlask"));
                }
            }

            if (npc.type == NPCID.DrManFly && Main.expertMode)
            {
                if (Main.rand.Next(0, 20) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("HeisenbergsFlask"));
                }
            }

            if (npc.type == NPCID.Stylist && !Main.expertMode)
            {
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Extase"));
                }
            }
            if (npc.type == NPCID.Stylist && Main.expertMode)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("Extase"));
                }
            }
            if (npc.type == NPCID.MoonLordCore && !Main.expertMode)
            {
                if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("StarMaelstorm"));
                }
            }
            if (npc.type == NPCID.MoonLordCore && Main.expertMode)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("StarMaelstorm"));
                }
            }
            if (npc.type == NPCID.RedDevil && !Main.expertMode)
            {
                if (Main.rand.Next(0, 15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ScarletFlareCore"));
                }
            }
            if (npc.type == NPCID.RedDevil && Main.expertMode)
            {
                if (Main.rand.Next(0, 10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("ScarletFlareCore"));
                }
            }
            if (npc.type == NPCID.Demon && !Main.expertMode)
            {
                if (Main.rand.Next(60) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DaedricSword"));
                }
            }
            if (npc.type == NPCID.Demon && Main.expertMode)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("DaedricSword"));
                }
            }
            if (npc.type == NPCID.Golem && !Main.expertMode)
            {
                if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SolBlade"));
                }
            }
            if (npc.type == NPCID.Golem && Main.expertMode)
            {
                if (Main.rand.Next(75) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<T>("SolBlade"));
                }
            }

        }*/
    }
}
