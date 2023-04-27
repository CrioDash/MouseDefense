using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public class EnemyMobik:Enemy
    {
        public override void SetStats()
        {
            SetWaypoints(Level.Instance.Waypoints.ToArray());
            if(GetComponent<IEnemyMove>()==null)
                Move = gameObject.AddComponent<DefaultMove>();
            if(GetComponent<ITakeDamage>()==null)
                TakeDamage = gameObject.AddComponent<DefaultTakeDamage>();
        }
        
    }
}