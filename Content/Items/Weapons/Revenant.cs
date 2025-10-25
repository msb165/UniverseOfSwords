using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Revenant : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Point this sword to the sky and unleash spirits of the dead'");
        }

        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Red;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item103 with { Volume = 0.4f };
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 50);
            Item.scale = 1.25f;
            Item.holdStyle = 0;
        }

        public override void HoldItem(Player player) => Item.holdStyle = ModContent.GetInstance<UniverseConfig>().enableHoldStyle ? 999 : 0;

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (ModContent.GetInstance<UniverseConfig>().enableHoldStyle)
            {
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 4f);
            }
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.Center;

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Cyan, 1.125f, (int)(18 * Item.scale), (int)(80 * Item.scale));
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (NPCID.Sets.CountsAsCritter[target.type] || target.immortal || !target.active)
            {
                return;
            }
            for (int i = 0; i < 3; i++) //Replace 2 with number of projectiles
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), player.MountedCenter, (-Vector2.UnitY * 10f).RotatedBy(i * MathHelper.TwoPi / 10f), ModContent.ProjectileType<RevenantProj>(), Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}