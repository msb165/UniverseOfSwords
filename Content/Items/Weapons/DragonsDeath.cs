using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Buffs;
using UniverseOfSwords.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class DragonsDeath : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragon's Death");
			// Tooltip.SetDefault("'Great for impersonating Dragon hunters'");
		}
		
        public override void SetDefaults()
        { 
            Item.Size = new(40);
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 40;
            Item.useAnimation = 40;           
            Item.damage = 150;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 1, silver: 45);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -62f), Vector2.Zero);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<TrueSlow>(), 30);
            target.AddBuff(BuffID.Poisoned, 200);
        }
    }
}