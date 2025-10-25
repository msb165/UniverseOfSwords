using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TheNightmareAmalgamation : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The source of your nightmares'");
        }

        public override void SetDefaults()
        {
            Item.Size = new(40);
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 20;
            Item.damage = 150;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item71;
            Item.shoot = ModContent.ProjectileType<Nightmare>();
            Item.shootSpeed = 5f;
            Item.value = Item.sellPrice(gold: 15);
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
                UniverseUtils.CustomHoldStyle(player, new Vector2(48f * player.direction, -52f), Vector2.UnitY);
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //Projectile.NewProjectile(source, position - Vector2.UnitY * 72f + velocity, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Purple, 1.25f, 30, 130);
            UniverseUtils.SpawnRotatedDust(player, DustID.Clentaminator_Purple, 1.25f, 30, 130);
            Lighting.AddLight(player.Center, Color.Purple.ToVector3());
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 newPos = Main.rand.NextVector2CircularEdge(199f, 199f);
                Vector2 spawnPos = target.Center - newPos;
                Vector2 newVel = (target.Center - spawnPos).SafeNormalize(Vector2.UnitY) * 7f;
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - newPos, newVel * Main.rand.NextFloat(1f, 1.25f), Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            }
            Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ModContent.ProjectileType<NightmareBlast>(), damageDone, Item.knockBack / 2, player.whoAmI, target.whoAmI);
            target.AddBuff(BuffID.ShadowFlame, 800);
            target.AddBuff(BuffID.Venom, 800);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CthulhuJudge>())
                .AddIngredient(ModContent.ItemType<StickyGlowstickSword>())
                .AddIngredient(ModContent.ItemType<TheEater>())
                .AddIngredient(ItemID.BeeKeeper)
                .AddIngredient(ModContent.ItemType<FixedSwordOfPower>())
                .AddIngredient(ItemID.BreakerBlade)
                .AddIngredient(ModContent.ItemType<PrimeSword>())
                .AddIngredient(ModContent.ItemType<DestroyerSword>())
                .AddIngredient(ModContent.ItemType<TwinsSword>())
                .AddIngredient(ModContent.ItemType<Executioner>())
                .AddIngredient(ModContent.ItemType<Golem>())
                .AddIngredient(ModContent.ItemType<Doomsday>())
                .AddIngredient(ModContent.ItemType<Sharkron>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CthulhuJudge>())
                .AddIngredient(ModContent.ItemType<StickyGlowstickSword>())
                .AddIngredient(ModContent.ItemType<TheBrain>())
                .AddIngredient(ItemID.BeeKeeper)
                .AddIngredient(ModContent.ItemType<FixedSwordOfPower>())
                .AddIngredient(ItemID.BreakerBlade)
                .AddIngredient(ModContent.ItemType<PrimeSword>())
                .AddIngredient(ModContent.ItemType<DestroyerSword>())
                .AddIngredient(ModContent.ItemType<TwinsSword>())
                .AddIngredient(ModContent.ItemType<Executioner>())
                .AddIngredient(ModContent.ItemType<Golem>())
                .AddIngredient(ModContent.ItemType<Doomsday>())
                .AddIngredient(ModContent.ItemType<Sharkron>())
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}