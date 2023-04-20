using UnityEngine;

namespace Enemies.Shielder
{
    public class EnemyShielder:Enemy
    {
        [Header("Настройки щитовика")] public float ShieldHP;
        public GameObject Shield;

        [HideInInspector] public float shieldHP;
        
        
        public override void SetStats()
        {
            shieldHP = ShieldHP;
            Shield.SetActive(true);
            SetWaypoints(Level.Instance.Waypoints.ToArray());
            Move = gameObject.AddComponent<DefaultMove>();
            TakeDamage = gameObject.AddComponent<IShielderTakeDamage>();
        }
    }
}