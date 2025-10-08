using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Doomsday : ModItem
    {
        public override void SetDefaults()
        { 
            Item.width = 66;
            Item.height = 70; 
			Item.scale = 1.25f;
            Item.rare = ItemRarityID.Yellow;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;   
            Item.useAnimation = 20; 			
            Item.damage = 100; 
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item45;
            Item.value = Item.sellPrice(gold: 4, silver: 70);		
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            base.MeleeEffects(player, hitbox);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center, Vector2.Zero, ModContent.ProjectileType<FlamesBlast>(), Item.damage, Item.knockBack, player.whoAmI);
            }
        }
	}
}