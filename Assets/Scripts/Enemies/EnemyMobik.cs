using UnityEngine;

namespace Enemies
{
    public class EnemyMobik:Enemy
    {
        public override void SetStats()
        {
            _move = gameObject.AddComponent<IDefaultMove>();
        }
        
    }
}