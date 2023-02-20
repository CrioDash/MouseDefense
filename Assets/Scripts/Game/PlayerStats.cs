using System.Collections.Generic;
using Consumables;

namespace Game
{
    public static class PlayerStats
    {
    

        //Bonus Stats
        public static int BonusHealth = 0;
        public static int BonusGold = 0;

        //Towers
        public static TowerType[] Towers = {TowerType.Gun, TowerType.Artillery, TowerType.AntiAir, TowerType.Corngun};

        public static Dictionary<Consumable.ConsumableType, int> Consumables =
            new Dictionary<Consumable.ConsumableType, int>();

    }
}
