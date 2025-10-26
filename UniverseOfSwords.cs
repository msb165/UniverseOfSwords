using Terraria.ModLoader;

namespace UniverseOfSwords
{
    public class UniverseOfSwords : Mod
    {
        public static UniverseOfSwords Instance;
        public Mod CalamityMod = null;
        public Mod ThoriumMod = null;
        public UniverseOfSwords()
        {
            
        }

        public override void Load()
        {
            Instance = this;
            ThoriumMod = null;
            ModLoader.TryGetMod("ThoriumMod", out ThoriumMod);
            CalamityMod = null;
            ModLoader.TryGetMod("CalamityMod", out CalamityMod);
            base.Load();
        }
    }
}