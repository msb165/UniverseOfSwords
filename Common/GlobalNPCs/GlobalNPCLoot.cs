using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Accessories;
using UniverseOfSwords.Content.Items.Consumables;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Items.Placeable;
using UniverseOfSwords.Content.Items.Weapons;

namespace UniverseOfSwords.Common.GlobalNPCs
{
    public class UniverseOfSwordsModGlobalNPC : GlobalNPC
    {

        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            if (Main.rand.NextBool(8))
            {
                shop[nextSlot] = ModContent.ItemType<Skooma>();
                nextSlot++;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            Conditions.IsHardmode hardMode = new();
            Conditions.NotExpert isNotExpert = new();            

            if (npc.lifeMax > 5 && !npc.immortal && !NPCID.Sets.CountsAsCritter[npc.type])
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordMatter>(), 6, 2, 15));
            }

            if (npc.boss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordMatter>(), 3, 5, 30));
                if (Main.hardMode)
                {
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<UpgradeMatter>(), 6, 2, 10));
                }
            }

            if (Array.IndexOf([NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail], npc.type) > -1)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.LegacyHack_IsABoss(), ModContent.ItemType<TheEater>()));
            }
            if (Array.IndexOf([NPCID.WyvernHead, NPCID.WyvernBody, NPCID.WyvernTail], npc.type) > -1)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SkyPower>(), 10));
            }

            switch (npc.type)
            {
                case NPCID.EyeofCthulhu:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<CthulhuJudge>()));
                    break;
                case NPCID.KingSlime:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<StickyGlowstickSword>()));
                    break;
                case NPCID.BrainofCthulhu:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<TheBrain>()));
                    break;
                case NPCID.SkeletronHead:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<SwordOfPower>()));
                    break;
                case NPCID.SkeletronPrime:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<PrimeSword>()));
                    break;
                case NPCID.Spazmatism:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<TwinsSword>()));
                    break;
                case NPCID.TheDestroyer:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<DestroyerSword>()));
                    break;
                case NPCID.Plantera:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<Executioner>()));
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<BlackBar>(), 1, 15, 30));
                    break;
                case NPCID.Golem:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<Golem>()));
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<SwordShard>(), 1, 2, 12));
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<SolBlade>(), 100));
                    break;
                case NPCID.CultistBoss:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Doomsday>()));
                    break;
                case NPCID.MoonLordCore:
                    npcLoot.Add(ItemDropRule.ByCondition(isNotExpert, ModContent.ItemType<StarMaelstorm>(), 30));
                    break;
                case NPCID.BigMimicHallow:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystalVileSword>(), 10));
                    break;
                case NPCID.BigMimicCorruption:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClingerSword>(), 10));
                    break;
                case NPCID.Zombie:
                case NPCID.ArmedZombie:
                case NPCID.FemaleZombie:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZombieKnife>(), 8));
                    break;
                case NPCID.Paladin:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PaladinSword>(), 8));
                    break;
                case NPCID.DungeonGuardian:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HaloOfHorrors>(), 100));
                    break;
                case NPCID.RedDevil:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DevilBlade>(), 10));
                    npcLoot.Add(ItemDropRule.ByCondition(new DownedGolem(), ModContent.ItemType<ScarletFlareCore>(), 10));
                    break;
                case NPCID.GreekSkeleton:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CaesarSword>(), 6));
                    break;
                case NPCID.BlackRecluse:
                case NPCID.BlackRecluseWall:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PoisonSword>(), 6));
                    break;
                case NPCID.Vampire:
                case NPCID.VampireBat:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DraculaSword>(), 10));
                    break;
                case NPCID.LunarTowerNebula:
                case NPCID.LunarTowerSolar:
                case NPCID.LunarTowerVortex:
                case NPCID.LunarTowerStardust:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BeliarClaw>(), 5));
                    break;
                case NPCID.GoblinSummoner:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PhantomScimitar>(), 6));
                    break;
                case NPCID.DrManFly:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HeisenbergsFlask>(), 5));
                    break;
                case NPCID.HellArmoredBonesSpikeShield:
                case NPCID.HellArmoredBones:
                case NPCID.HellArmoredBonesMace:
                case NPCID.HellArmoredBonesSword:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordOfFlames>(), 3));
                    break;
                case NPCID.BlueArmoredBones:
                case NPCID.BlueArmoredBonesMace:
                case NPCID.BlueArmoredBonesNoPants:
                case NPCID.BlueArmoredBonesSword:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagnetSword>(), 8));
                    break;
                case NPCID.Harpy:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FeatherDuster>(), 3));
                    break;
                case NPCID.Frankenstein:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FingerOfDoom>(), 5));
                    break;
                case NPCID.Stylist:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Extase>(), 10));
                    break;
                case NPCID.GraniteGolem:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WitherBane>(), 4));
                    break;
                case NPCID.Wraith:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WraithBlade>(), 4));
                    break;
                case NPCID.DarkCaster:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterBoltSword>(), 5));
                    break;
                case NPCID.MossHornet:
                case NPCID.Arapaima:
                case NPCID.FlyingSnake:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonsDeath>(), 100));
                    break;
                case NPCID.Mimic:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElBastardo>(), 20));
                    break;
                case NPCID.Crab:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OceanRoar>(), 5));
                    break;
                case NPCID.PossessedArmor:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PossessedSword>(), 5));
                    break;
                case NPCID.FireImp:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fireball>(), 20));
                    break;
                case NPCID.GiantBat:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BatSlayer>(), 15));
                    break;
                case NPCID.Piranha:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Biter>(), 15));
                    break;
                case NPCID.GoblinPeon:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoblinKnife>(), 15));
                    break;
                case NPCID.IchorSticker:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IchorBlade>(), 25));
                    break;
                case NPCID.MartianSaucerCore:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MartianSaucerCore>(), 3));
                    break;
            }
        }
    }
}
