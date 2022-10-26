using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyParatrooper : Enemy
    {
        [Header("Настройки парашютиста")] public int ParashootHP;
        
        private GameObject _parashoot;
        public override void SetStats()
        {
            Move = gameObject.AddComponent<IParatroopMove>();
            TakeDamage = gameObject.AddComponent<IParatrooperTakeDamage>();
            _parashoot = transform.Find("Parashoot").gameObject;
        }

        private void Update()
        {
           
        }
    }
}