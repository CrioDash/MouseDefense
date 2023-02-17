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
            Move = gameObject.AddComponent<IDefaultMove>();
            TakeDamage = gameObject.AddComponent<IShielderTakeDamage>();
        }
    }
}