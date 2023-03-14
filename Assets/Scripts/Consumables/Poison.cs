using UnityEngine;

namespace Consumables
{
    public class Poison:Consumable
    {
        private void Start()
        {
            _type = ConsumableType.Poison;
        }
        
        public override void Activate()
        {
            
        }
    }
}