using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;
using static Terraria.ModLoader.ModContent;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TrueGemSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.shoot = ProjectileType<Projectiles.Held.TrueGemSword>();
            Item.shootSpeed = 1f;
            Item.damage = 80;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 20);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }

        public override bool MeleePrefix() => true;

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.GemAmber);
                UniverseUtils.SpawnRotatedDust(player, DustID.GemAmethyst);
                UniverseUtils.SpawnRotatedDust(player, DustID.GemDiamond);
                UniverseUtils.SpawnRotatedDust(player, DustID.GemEmerald);
                UniverseUtils.SpawnRotatedDust(player, DustID.GemRuby);
                UniverseUtils.SpawnRotatedDust(player, DustID.GemSapphire);
                UniverseUtils.SpawnRotatedDust(player, DustID.GemTopaz);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemType<GemSlayer>())
                .AddIngredient(ItemID.BrokenHeroSword)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        int swingDirection = 1;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swingDirection *= -1;
            Projectile.NewProjectile(source, position, Vector2.Normalize(velocity), Item.shoot, damage, knockback, player.whoAmI, ai1: swingDirection);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Midas, 360);
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 8f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ProjectileType<GemBolt>(), (int)(damageDone * 0.8), hit.Knockback, player.whoAmI, ai0: Main.rand.Next(6));
            int projAmount = Main.rand.Next(3, 6);
            for (int i = 0; i < projAmount; i++)
            {
                Vector2 velocity = Utils.RandomVector2(Main.rand, -100f, 101f);
                while (velocity.Equals(Vector2.Zero))
                {
                    velocity = Utils.RandomVector2(Main.rand, -100f, 101f);
                }
                velocity = velocity.SafeNormalize(-Vector2.UnitY) * Main.rand.Next(70, 101) * 0.1f;
                if (Collision.SolidTiles(target.Center - velocity * 10f, 10, 10))
                {
                    continue;
                }
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - velocity * 10f, velocity, ProjectileType<GemPulse>(), (int)(damageDone * 0.5), hit.Knockback * 0.8f, player.whoAmI);
            }
        }
    }
}