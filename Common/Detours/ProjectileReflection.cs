using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UniverseOfSwordsMod.Common.GlobalItems;

namespace UniverseOfSwordsMod.Common.Detours
{
    public partial class DetourChanges : ModSystem
    {
        static Rectangle itemRec = new();
        static bool noAttack = false;

        private void On_Player_ItemCheck_GetMeleeHitbox(On_Player.orig_ItemCheck_GetMeleeHitbox orig, Player self, Item sItem, Rectangle heldItemFrame, out bool dontAttack, out Rectangle itemRectangle)
        {
            orig(self, sItem, heldItemFrame, out dontAttack, out itemRectangle);
            noAttack = dontAttack;
            itemRec = itemRectangle;
        }

        public static void AddProjectileReflection(On_Player.orig_ItemCheck_OwnerOnlyCode orig, Player self, ref Player.ItemCheckContext context, Item sItem, int weaponDamage, Rectangle heldItemFrame)
        {
            orig(self, ref context, sItem, weaponDamage, heldItemFrame);
            if (self.ItemAnimationActive)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (Main.myPlayer == self.whoAmI && !noAttack && !itemRec.IsEmpty && itemRec.Intersects(proj.Hitbox))
                    {
                        bool canBeReflected = !proj.reflected && proj.hostile;
                        if (!(Main.rand.Next(1, 101) <= sItem.GetGlobalItem<ReflectionChance>().reflectChance) || !canBeReflected)
                        {
                            return;
                        }

                        SoundEngine.PlaySound(SoundID.Item150, self.Center);
                        proj.velocity = -proj.oldVelocity;
                        proj.friendly = true;
                        proj.hostile = false;
                        proj.reflected = true;
                    }
                }
            }
        }
    }
}
