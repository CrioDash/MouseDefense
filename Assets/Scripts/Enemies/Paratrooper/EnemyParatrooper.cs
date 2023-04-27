using System;
using Enemies.Paratrooper;
using UnityEngine;

namespace Enemies
{
    public class EnemyParatrooper : Enemy
    {
        [Header("Настройки парашютиста")] public float ParashootHP;

        [HideInInspector] public float parashootHP;
        
        public GameObject parashoot;
        public override void SetStats()
        {
            if(GetComponent<IEnemyMove>()==null)
                Move = gameObject.AddComponent<IParatroopMove>();
            if(GetComponent<ITakeDamage>() == null)
             TakeDamage = gameObject.AddComponent<IParatrooperTakeDamage>();
        }
        
    }
}