using UnityEngine;

namespace Enemies.Shielder
{
    public class EnemyShielder:Enemy
    {
        [Header("Настройки щитовика")] public float ShieldHP;
        public GameObject Shield;
        
        
        public override void SetStats()
        {
            SetWaypoints(Level.currentLevel.Waypoints.ToArray());
            Move = gameObject.AddComponent<DefaultMove>();
            TakeDamage = gameObject.AddComponent<IShielderTakeDamage>();
        }
    }
}