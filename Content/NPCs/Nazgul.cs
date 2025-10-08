using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using UniverseOfSwords.Content.Items.Weapons;

namespace UniverseOfSwords.Content.NPCs
{
    public class Nazgul : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
            NPCID.Sets.TrailCacheLength[Type] = 11;
            NPCID.Sets.TrailingMode[Type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 54;
            NPC.damage = 65;
            NPC.defense = 24;
            NPC.lifeMax = 200;
            NPC.HitSound = SoundID.NPCHit56;
            NPC.DeathSound = SoundID.NPCDeath56;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.aiStyle = NPCAIStyleID.Fighter;
            AnimationType = NPCID.Zombie;
            NPC.knockBackResist = 0.1f;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Ichor] = true;
            NPC.buffImmune[BuffID.OnFire] = false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.CursedInferno, 600, true);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.WrathPotion, 5, 2, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WitchKingsDaughter>(), 20));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneOverworldHeight && Main.hardMode ? SpawnCondition.OverworldNightMonster.Chance * 0.01f : 0f;

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 40; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SilverCoin, 0.1f * hit.HitDirection, 0.1f, 0);
                }
            }
        }

        public override bool ModifyCollisionData(Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox)
        {
            int offset = 34;
            if (NPC.spriteDirection < 0)
            {
                npcHitbox.X -= offset;
            }
            npcHitbox.Width += offset;
            damageMultiplier *= 1.25f;
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[Type].Value;
            Vector2 halfSize = texture.Size() / Main.npcFrameCount[Type] / 2;
            SpriteEffects spriteEffects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Color npcColor = NPC.GetAlpha(drawColor);
            Color trailColor = npcColor;
            Vector2 drawPos = new(NPC.position.X + (NPC.spriteDirection == -1 ? 0f : NPC.Size.X) - screenPos.X - texture.Width * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.position.Y - screenPos.Y + NPC.height - texture.Height * NPC.scale / Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale);

            for (int i = 1; i < NPC.oldPos.Length; i++)
            {
                Vector2 trailPos = new(NPC.oldPos[i].X + (NPC.spriteDirection == -1 ? 0f : NPC.Size.X) - screenPos.X - texture.Width * NPC.scale / 2f + halfSize.X * NPC.scale, NPC.oldPos[i].Y - screenPos.Y + NPC.height - texture.Height * NPC.scale / Main.npcFrameCount[Type] + 4f + halfSize.Y * NPC.scale);
                trailColor.R = (byte)(100 * (10 - i) / 15);
                trailColor.G = (byte)(100 * (10 - i) / 15);
                trailColor.B = (byte)(150 * (10 - i) / 15);
                trailColor.A = (byte)(50 * (10 - i) / 15);
                spriteBatch.Draw(texture, trailPos, NPC.frame, trailColor, NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
            }
            spriteBatch.Draw(texture, drawPos, NPC.frame, npcColor, NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
            return false;
        }
    }
}
