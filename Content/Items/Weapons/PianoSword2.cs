using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class PianoSword2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gershwin Gasher");
            // Tooltip.SetDefault("'Rhapsody in Blue - Gershwin'");
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Cyan;
            Item.crit = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 60;
            Item.useAnimation = 30;
            Item.damage = 18;
            Item.knockBack = 8f;
            Item.UseSound = new SoundStyle($"{nameof(UniverseOfSwords)}/Assets/Sounds/Item/PianoBlue");
            Item.shoot = ProjectileID.Mushroom;
            Item.shootSpeed = 3f;
            Item.value = Item.sellPrice(silver: 80);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
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

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation = player.Center;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MushroomPiano, 1);
            recipe.AddIngredient(ItemID.GranitePiano, 1);
            recipe.AddIngredient(ItemID.MarblePiano, 1);
            recipe.AddIngredient(ItemID.PumpkinPiano, 1);
            recipe.AddIngredient(ItemID.DynastyPiano, 1);
            recipe.AddIngredient(ItemID.FrozenPiano, 1);
            recipe.AddIngredient(ItemID.GlassPiano, 1);
            recipe.AddIngredient(ItemID.HoneyPiano, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                UniverseUtils.SpawnRotatedDust(player, DustID.GoblinSorcerer, 1.5f, alpha: 0);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < Main.rand.Next(1, 4); i++)
            {
                Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.75f, 1.25f);
                Projectile.NewProjectile(source, position + newVel * 8f, newVel, ModContent.ProjectileType<Note>(), damage / 2, knockback, player.whoAmI, ai1: Main.rand.Next(0, 3), ai2: Main.rand.NextFloat(0.1f, 0.4f));
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 360);
            target.AddBuff(BuffID.Frostburn, 360);
            target.AddBuff(BuffID.Venom, 360);
        }
    }
}