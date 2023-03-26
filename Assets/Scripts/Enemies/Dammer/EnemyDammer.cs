using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Dammer
{
    public class EnemyDammer : Enemy
    {
        public GameObject dam;
        public GameObject damPrefab;

        public List<Transform> DamPoints { set; get; }
        
        public override void SetStats()
        {
            SetWaypoints(Level.Instance.DammerPositions.ToArray());
            DamPoints = Level.Instance.DamPositions;
           
            Move = gameObject.AddComponent<IDammerMove>();
            TakeDamage = gameObject.AddComponent<DefaultTakeDamage>();
        }
    }
}