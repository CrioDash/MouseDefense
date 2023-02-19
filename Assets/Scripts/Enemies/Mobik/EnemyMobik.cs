using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public class EnemyMobik:Enemy
    {
        public override void SetStats()
        {
            SetWaypoints(Level.currentLevel.Waypoints.ToArray());
            Move = gameObject.AddComponent<DefaultMove>();
            TakeDamage = gameObject.AddComponent<DefaultTakeDamage>();
        }
        
    }
}