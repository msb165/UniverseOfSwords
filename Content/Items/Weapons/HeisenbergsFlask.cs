using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common;
using UniverseOfSwords.Content.Projectiles.Common;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Items.Weapons
{
    public class HeisenbergsFlask : ModItem
    {
		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Heisenberg's Flask");
			// Tooltip.SetDefault("'Hablan de un tal Heisenberg que ahora controla el mercado'");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 28;
            Item.height = 28; 
			Item.scale = 2f;
            Item.rare = ItemRarityID.Cyan;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 20;   
            Item.useAnimation = 20; 			
            Item.damage = 50; 
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item107;
            Item.value = Item.sellPrice(gold: 2);					
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

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target))
            {
                for (int i = 0; i < Main.rand.Next(20, 31); i++)
                {
                    Vector2 newVel = Vector2.Normalize(Utils.RandomVector2(Main.rand, -100, 101)) * Main.rand.Next(10, 201) * 0.01f;
                    Projectile.NewProjectileDirect(target.GetSource_OnHit(target), target.Center, newVel, ModContent.ProjectileType<BlueCloud1>(), Item.damage, Item.knockBack, player.whoAmI);
                }
            }
        }
    }
}