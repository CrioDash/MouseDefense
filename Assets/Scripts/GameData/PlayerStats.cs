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
        

        //Towers
        public Dictionary<TowerType, int> Towers = new Dictionary<TowerType, int>();

        public Dictionary<Consumable.ConsumableType, int> Consumables =
            new Dictionary<Consumable.ConsumableType, int>();

        public PlayerStats()
        {
            Towers.Add(TowerType.Gun, 1);
            RenderScale = 1f;
        }
        
    }
    
        
}
