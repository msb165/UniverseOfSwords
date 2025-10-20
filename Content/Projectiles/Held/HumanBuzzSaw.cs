using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwords.Common.GlobalItems;
using UniverseOfSwords.Utilities;

namespace UniverseOfSwords.Content.Projectiles.Held     //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class HumanBuzzSaw : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Items.Weapons.HumanBuzzSaw>().Texture;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Human Buzz Saw");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            Projectile.width = 106;     //Set the hitbox width
            Projectile.height = 106;
            Projectile.scale = 1.125f;
            Projectile.friendly = true;    //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1;    //Tells the game how many enemies it can hit before being destroyed. -1 = never
            Projectile.tileCollide = false; //Tells the game whether or not it can collide with a tile
            Projectile.ignoreWater = true; //Tells the game whether or not projectile will be affected by water        
            Projectile.DamageType = DamageClass.Melee;  //Tells the game whether it is a melee projectile or not
            Projectile.noEnchantmentVisuals = true;
        }
        Player Player => Main.player[Projectile.owner];
        public override void AI()
        {
            Projectile.soundDelay--;
            if (Projectile.soundDelay <= 0)
            {
                Projectile.soundDelay = 50; 
                SoundEngine.PlaySound(SoundID.Item22, Projectile.position);
                SoundEngine.PlaySound(SoundID.Item23, Projectile.position);
            }

            if (!Player.channel || Player.noItems || Player.CCed)
            {
                Projectile.Kill();
            }

            Vector2 speed = Vector2.Normalize(Main.MouseWorld - Player.Center);
            Projectile.Center = Player.MountedCenter + speed * 4f;
            Projectile.spriteDirection = Player.direction;
            Projectile.rotation += 0.3f * Player.direction; //this is the projectile rotation/spinning speed
            if (Projectile.rotation > MathHelper.TwoPi)
            {
                Projectile.rotation -= MathHelper.TwoPi;
            }
            else if (Projectile.rotation < 0)
            {
                Projectile.rotation += MathHelper.TwoPi;
            }
            if (Main.myPlayer == Projectile.owner)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.whoAmI != Projectile.whoAmI && Projectile.Colliding(Projectile.Hitbox, proj.Hitbox) && !proj.reflected && proj.hostile && Main.rand.Next(1, 100) <= Player.HeldItem.GetGlobalItem<ReflectionChance>().reflectChance)
                    {
                        SoundEngine.PlaySound(SoundID.Item150, Projectile.Center);
                        proj.velocity = -proj.oldVelocity;
                        proj.friendly = true;
                        proj.hostile = false;
                        proj.reflected = true;
                    }
                }
            }
            SetPlayerValues();
        }

        public void SetPlayerValues()
        {
            Player.SetDummyItemTime(2);
            Player.heldProj = Projectile.whoAmI;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (UniverseUtils.IsAValidTarget(target) && Projectile.CanHitWithMeleeWeapon(target) && Main.myPlayer == Projectile.owner)
            {
                NPCLoader.OnHitByItem(target, Player, Player.HeldItem, hit, damageDone);
            }
        }


        public override bool PreDraw(ref Color lightColor)  //this make the projectile sprite rotate perfectaly around the player
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Color color = Projectile.GetAlpha(lightColor);
            Vector2 origin = texture.Size() / 2;
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, color, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}