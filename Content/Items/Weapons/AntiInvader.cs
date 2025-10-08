using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class AntiInvader : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Anti-Invader");
            // Tooltip.SetDefault("'I'll like to have chicken burger, fried chicken wings, two slices of your chicken pie, no make that three... What do you mean you can't supersize me?!'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(64);
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 100;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item33;
            Item.value = Item.sellPrice(gold: 25);
            Item.scale = 1.4f;
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -64f), Vector2.UnitY * 6f);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (NPCID.Sets.CountsAsCritter[target.type] && target.immortal && !target.chaseable && target.dontTakeDamage)
            {
                return;
            }

            for (int i = 0; i < 6; i++) //Replace 2 with number of projectiles
            {
                UniverseUtils.Spawn.SummonGenericSlash(target.Center, new(100, 220, 255), player.whoAmI, damageDone, 180, 0.5f);
            }

            if (target.life <= 0)
            {
                Projectile.NewProjectileDirect(target.GetSource_Death(), target.Center, Vector2.Zero, ProjectileID.MonkStaffT3_AltShot, (int)(damageDone * 1.5), Item.knockBack, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
             CreateRecipe()
                .AddIngredient(ItemID.MartianConduitPlating, 1000)
                .AddIngredient(ModContent.ItemType<MartianSaucerCore>())
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 200)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}