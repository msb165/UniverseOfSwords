using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class BladesOfBalance : ModItem
    {
        public override void SetStaticDefaults()
        {
		    // DisplayName.SetDefault("Blades of Balance");
        }

        public override void SetDefaults()
        {
            Item.damage = 51;
			Item.crit = 2;
            Item.DamageType = DamageClass.Melee;
            Item.width = 54;
            Item.height = 54;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7.0F;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.LightPurple;
			Item.scale = 1.2F;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{

                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PinkTorch, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X += player.direction * 0f;
                Main.dust[dust].velocity.Y += 0.0f;
                dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.VilePowder, 0f, 0f, 100, default, 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity.X += player.direction * 0f;
                Main.dust[dust].velocity.Y += 0.0f;
			}
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
			recipe.AddIngredient(ItemID.FrostCore, 1);
			recipe.AddIngredient(ItemID.LightShard, 1);
			recipe.AddIngredient(ItemID.DarkShard, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);			
            recipe.Register();
	    }
    }
}