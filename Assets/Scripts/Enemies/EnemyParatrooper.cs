using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyParatrooper : Enemy
    {
        private GameObject _parashoot;
        public override void SetStats()
        {
            _move = gameObject.AddComponent<IParatroopMove>();
            _parashoot = transform.Find("Parashoot").gameObject;
        }

        private void Update()
        {
           
        }
    }
}