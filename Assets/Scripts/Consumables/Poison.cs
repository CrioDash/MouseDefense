using GameData;
using UnityEngine;

namespace Consumables
{
    public class Poison:Consumable
    {
        private void Start()
        {
            _type = ConsumableType.Poison;
            Radius = PlayerStats.Instance.PoisonRadius;
            ColorCircle.transform.localScale = new Vector3(Radius, Radius, 1); 
            RadiusCircle.transform.localScale = new Vector3(Radius, Radius, 1);
        }
        
        public override void Activate()
        {
           GameObject gm = Instantiate(ConsumablePrefab);
           gm.transform.position = ConsumableRaycast.point;
        }
    }
}