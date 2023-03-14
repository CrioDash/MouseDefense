using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Consumables
{
    public class Bomb:Consumable
    {
        private void Start()
        {
            _type = ConsumableType.Bomb;
        }

        public override void Activate()
        {
            
        }
    }
}