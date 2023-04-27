using System;
using System.Collections.Generic;
using Consumables;
using GameData;
using UnityEngine;
using TowerType = GameData.Variables.TowerType;

namespace GameData
{
    [Serializable]
    public class PlayerStats
    {
        public static PlayerStats Instance;
        
        //Settings
        public bool IsShadows { set; get; }

        public float RenderScale { set; get; }

        //Bonus Stats
        public int BonusHealth = 0;
        public int BonusGold = 0;
        
        //Consumables
        public int PoisonDamage = 0;
        public float PoisonRadius = 0;

        //Towers
        public Dictionary<TowerType, int> Towers = new Dictionary<TowerType, int>();

        public Dictionary<Consumable.ConsumableType, int> Consumables =
            new Dictionary<Consumable.ConsumableType, int>();

        public PlayerStats()
        {
            Towers.Add(TowerType.Gun, 3);
            Consumables.Add(Consumable.ConsumableType.Bomb, 3);
            Consumables.Add(Consumable.ConsumableType.Poison, 3);
            IsShadows = true;
            RenderScale = 1f;
            BonusHealth = 0;
            BonusGold = 0;
            PoisonDamage = 2;
            PoisonRadius = 12;
        }
        
    }
    
        
}
