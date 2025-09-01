using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Content.Projectiles.Common;
using UniverseOfSwordsMod.Utilities;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class CthulhuJudge : ModItem
    {
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("I have an eye on you...");
		}
		
        public override void SetDefaults()
        { 
            Item.width = 58;
            Item.height = 60; 
			Item.scale = 1f;
            Item.rare = ItemRarityID.Orange;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 30;           
            Item.damage = 26; 
            Item.knockBack = 6.5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 20);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            UniverseUtils.SpawnRotatedDust(player, DustID.Blood, 2f, 18, 80, alpha: 200);
        }

        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<EyeOfCthulhu>()] < 1)
            {
                Vector2 newVel = Vector2.Normalize(Utils.RandomVector2(Main.rand, -100f, 101f));
                Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.Center, newVel, ModContent.ProjectileType<EyeOfCthulhu>(), Item.damage / 2, 4f, player.whoAmI);
            }
        }
    }
}
