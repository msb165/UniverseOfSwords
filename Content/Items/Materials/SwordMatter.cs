using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace UniverseOfSwords.Content.Items.Materials
{
    public class SwordMatter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            // DisplayName.SetDefault("Sword Matter");
            // Tooltip.SetDefault("'Matter of all swords'");
            //Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4));
            //ItemID.Sets.AnimatesAsSoul[Type] = true;
            ItemID.Sets.ItemIconPulse[Type] = true;
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.Size = new(20);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 0;
            Item.rare = ItemRarityID.Orange;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Type].Value;
            Vector2 drawOrigin = texture.Frame().Size() / 2;
            Vector2 drawPos = Item.position - Main.screenPosition + drawOrigin + new Vector2(Item.width / 2 - drawOrigin.X, Item.height - texture.Frame().Height);
            float timer = (float)Main.timeForVisualEffects / 120f;
            float timerGlow = MathF.Cos((float)Main.timeForVisualEffects * MathHelper.TwoPi / 30f);
            for (int i = 0; i < 8f; i++)
            {
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 1f).RotatedBy(MathHelper.PiOver4 * i) * (4f + 1f * timerGlow), texture.Frame(), Color.White with { A = 0 } * 0.25f, rotation, drawOrigin, scale, SpriteEffects.None, 0f);
            }
            for (float i = 0f; i < 1f; i += 0.25f)
            {
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 5f).RotatedBy(MathHelper.Pi * timer * i), texture.Frame(), Color.White with { A = 0 } * 0.5f, rotation, drawOrigin, scale, SpriteEffects.None, 0f);
            }
            for (float j = 0f; j < 1f; j += 0.34f)
            {
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(MathHelper.TwoPi * j * timer), texture.Frame(), new Color(140, 120, 255, 0) * 0.5f, rotation, drawOrigin, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(-MathHelper.TwoPi * j * timer), texture.Frame(), new Color(140, 120, 255, 0) * 0.5f, rotation, drawOrigin, scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}