using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PhantomScimitar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Phantom Scimitar");
            // Tooltip.SetDefault("Inflicts Shadowflame on hit");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 56;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 40;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item104;
            Item.value = Item.sellPrice(gold: 3);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.Y -= 1f * player.gravDir;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                for (int i = 0; i < 3; i++)
                {
                    UniverseUtils.SpawnRotatedDust(player, DustID.Shadowflame, 2f);
                }
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 400);
        }
    }
}