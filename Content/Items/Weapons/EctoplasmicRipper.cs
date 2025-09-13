using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class EctoplasmicRipper : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Steals mana points upon hit");
        }

        public override void SetDefaults()
        {
            Item.damage = 72;
            Item.crit = 2;
            Item.DamageType = DamageClass.Melee;
            Item.width = 54;
            Item.height = 54;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.0F;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ItemRarityID.Cyan;
            Item.scale = 1.0F;
            Item.UseSound = SoundID.Item103;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(1) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.MagicMirror, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddIngredient(ItemID.SpectreBar, 10);
            recipe.AddIngredient(null, "DeathSword", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int healingAmt = hit.Damage / 8;
            player.statMana += healingAmt;
            player.HealEffect(healingAmt, true);
        }
    }
}