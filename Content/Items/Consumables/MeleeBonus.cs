using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;

namespace UniverseOfSwords.Content.Items.Consumables
{
    public class MeleeBonus : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatShouldNotBeInInventory[Type] = true;
            ItemID.Sets.ItemNoGravity[Type] = true;
            ItemID.Sets.IsAPickup[Type] = true;
            ItemID.Sets.IgnoresEncumberingStone[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.Size = new(12);
        }

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.Grab);
            player.AddBuff(ModContent.BuffType<MeleeBooster1>(), 420);
            return false;
        }
    }
}
