using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwordsMod.Content.Items.Weapons
{
    public class DraculaSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 15;
            Item.damage = 43;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            //Item.shoot = ProjectileID.VampireKnife;
            //Item.shootSpeed = 8f;
            Item.value = Item.sellPrice(gold: 5);
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (NPCID.Sets.CountsAsCritter[target.type] || target.immortal || !target.active)
            {
                return;
            }
            target.AddBuff(BuffID.Bleeding, 360);
            for (int i = 0; i < 3; i++)
            {
                Vector2 spawnVel = (Vector2.UnitY * 16f).RotatedBy(i * MathHelper.TwoPi / 3f);
                Projectile.NewProjectile(target.GetSource_OnHit(target), target.Center - spawnVel * 10f, spawnVel, ProjectileID.VampireKnife, Item.damage, 4f, player.whoAmI);
            }
        }
    }
}