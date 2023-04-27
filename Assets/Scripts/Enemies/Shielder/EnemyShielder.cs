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
            if(GetComponent<IEnemyMove>()==null)
                Move = gameObject.AddComponent<DefaultMove>();
            if(GetComponent<ITakeDamage>()==null)
                TakeDamage = gameObject.AddComponent<IShielderTakeDamage>();
        }
    }
}