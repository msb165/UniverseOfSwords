using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class Machine : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Pew, pew! Boom, boom!");
        }
        
        public override void SetDefaults()
        {
            Item.width = 31;
            Item.height = 31; 
            Item.scale = 1.25f;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;
            Item.useAnimation = 20;           
            Item.damage = 62; 
            Item.knockBack = 3.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 10);
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) => player.itemLocation = player.MountedCenter;

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && player.CanHitNPCWithMeleeHit(target.whoAmI) && player.attackCD <= 0)
            {
                Vector2 newVel = (target.Center - player.Center).SafeNormalize(Vector2.Zero) * 2f;
                Projectile.NewProjectileDirect(target.GetSource_OnHit(target), player.Center, newVel, ProjectileID.VortexBeaterRocket, hit.Damage, hit.Knockback, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
        }
    }
}
