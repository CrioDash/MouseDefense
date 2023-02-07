using System;
using Enemies.Paratrooper;
using UnityEngine;

namespace Enemies
{
    public class EnemyParatrooper : Enemy
    {
        [Header("Настройки парашютиста")] public float ParashootHP;
        
        public GameObject parashoot;
        public override void SetStats()
        {
            Move = gameObject.AddComponent<IParatroopMove>();
            TakeDamage = gameObject.AddComponent<IParatrooperTakeDamage>();
        }
        
    }
}