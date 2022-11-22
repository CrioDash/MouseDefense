﻿using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyParatrooper : Enemy
    {
        [Header("Настройки парашютиста")] public int ParashootHP;
        
        public GameObject parashoot;
        public override void SetStats()
        {
            Move = gameObject.AddComponent<IParatroopMove>();
            TakeDamage = gameObject.AddComponent<IParatrooperTakeDamage>();
        }
        
    }
}