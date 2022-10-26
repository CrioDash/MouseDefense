using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public class EnemyMobik:Enemy
    {
        public override void SetStats()
        {
            Move = gameObject.AddComponent<IDefaultMove>();
            TakeDamage = gameObject.AddComponent<IDefaultTakeDamage>();
        }
        
    }
}