using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Consumables
{
    public class PurpleHeart : ModItem
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
            Item.Size = new(18);
            Item.alpha = 80;
        }

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.Grab);
            player.Heal(40);
            return false;
        }
    }
}
