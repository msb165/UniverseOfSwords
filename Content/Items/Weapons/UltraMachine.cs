using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Items.Materials;
using UniverseOfSwords.Content.Items.Placeable;
using UniverseOfSwords.Content.Projectiles.Common;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class UltraMachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ultra Machine");
            // Tooltip.SetDefault("'Insert Hollywood computer generated special effects here'");
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.scale = 1.4f;
            Item.rare = ItemRarityID.Red;
            Item.crit = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.damage = 120;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item62;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.UltraMachine>();
            Item.shootSpeed = 1f;
            Item.value = Item.sellPrice(platinum: 1);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            /*Projectile.NewProjectile(source, position, velocity, ProjectileID.LaserMachinegunLaser, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ProjectileID.RocketI, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y + 2, velocity.X, velocity.Y + 2, ProjectileID.RocketI, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y + 2, velocity.X, velocity.Y + 2, ProjectileID.LaserMachinegunLaser, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y + 2, velocity.X, velocity.Y + 2, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y - 2, velocity.X, velocity.Y - 2, ProjectileID.RocketI, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y - 2, velocity.X, velocity.Y - 2, ProjectileID.LaserMachinegunLaser, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position.X, position.Y - 2, velocity.X, velocity.Y - 2, ProjectileID.VortexBeaterRocket, damage, knockback, player.whoAmI);*/
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (NPCID.Sets.CountsAsCritter[target.type] || target.immortal || !target.active)
            {
                return;
            }

            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + Utils.RandomVector2(Main.rand, -200f, 200f), Vector2.Zero, ModContent.ProjectileType<MachineProbe>(), Item.damage, 4f, player.whoAmI);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<UpgradeMatter>(), 200);
            recipe.AddIngredient(ModContent.ItemType<DamascusBar>(), 20);
            recipe.AddIngredient(ItemID.SkeletronPrimeTrophy);
            recipe.AddIngredient(ItemID.DestroyerTrophy);
            recipe.AddIngredient(ItemID.RetinazerTrophy);
            recipe.AddIngredient(ItemID.SpazmatismTrophy);
            recipe.AddIngredient(ItemID.SpectreBar, 20);
            recipe.AddIngredient(ModContent.ItemType<PrimeSword>());
            recipe.AddIngredient(ModContent.ItemType<DestroyerSword>());
            recipe.AddIngredient(ModContent.ItemType<TwinsSword>());
            recipe.AddIngredient(ModContent.ItemType<MartianSaucerCore>());
            recipe.AddIngredient(ItemID.ShroomiteBar, 10);
            recipe.AddIngredient(ModContent.ItemType<SwordShard>(), 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}