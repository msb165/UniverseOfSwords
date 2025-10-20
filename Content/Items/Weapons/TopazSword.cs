using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;
using static UniverseOfSwords.Content.Projectiles.Common.GemBolt.GemType;


namespace UniverseOfSwords.Content.Items.Weapons
{
    public class TopazSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 15; 
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 20);			
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
	    }
	   
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
                UniverseUtils.SpawnRotatedDust(player, DustID.GemTopaz, 2f, (int)(12 * Item.scale), (int)(64 * Item.scale));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!UniverseUtils.IsAValidTarget(target))
            {
                return;
            }
            Vector2 newVel = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 4f;
            Projectile.NewProjectile(target.GetSource_OnHit(target), player.Center + newVel, newVel, ModContent.ProjectileType<GemBolt>(), (int)(damageDone * 0.75), hit.Knockback, player.whoAmI, ai0: (float)Gem_Topaz);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Topaz, 5)
                .AddTile(TileID.Anvils)
                .Register();
	    }
    }
}
