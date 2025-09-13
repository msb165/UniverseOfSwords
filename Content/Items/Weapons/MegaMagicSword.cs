using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using UniverseOfSwordsMod.Content.Items.Materials;
using Terraria.DataStructures;
using UniverseOfSwordsMod.Utilities;
using Microsoft.Xna.Framework.Graphics;
using UniverseOfSwordsMod.Content.Projectiles.Common;


namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class MegaMagicSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64; 
			Item.scale = 1.3F;
            Item.rare = ItemRarityID.Lime;            
            Item.useStyle = ItemUseStyleID.Swing;             
            Item.useTime = 30;
            Item.useAnimation = 12;           
            Item.damage = 82; 
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item109 with { Volume = 0.3f };
            Item.shoot = ModContent.ProjectileType<MagicProj>();
            Item.shootSpeed = 7f;
            Item.value = Item.sellPrice(gold: 4, silver: 10);			
            Item.autoReuse = true; 
            Item.DamageType = DamageClass.Melee;
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float piOver10 = MathHelper.PiOver4;
            Vector2 spawnPos = Vector2.Normalize(velocity) * 40f;
            for (int i = 0; i < 3; i++)
            {
                float offset = i - (3f - 1f) / 2f;
                Projectile.NewProjectile(source, position + velocity * 4f - Vector2.UnitY * 24f + spawnPos.RotatedBy(piOver10 * offset), velocity, type, damage / 2, player.whoAmI);
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 360);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }

        
		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BrokenHeroSword, 1)
                .AddIngredient(null, "MagicSword", 1)
                .AddIngredient(null, "Orichalcon", 1)
                .AddIngredient(ModContent.ItemType<SwordMatter>(), 200)
                .AddTile(TileID.MythrilAnvil)
                .Register();
	    }
    }
}