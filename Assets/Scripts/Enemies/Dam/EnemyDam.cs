using System;
using Enemies;
using Enemies.Dam;
using Enemies.SpecialEnemies;
using Unity.AI.Navigation;
using UnityEngine;

namespace Utilities
{
    public class EnemyDam:Enemy
    {
        private NavMeshSurface _surface;

        public override void SetStats()
        {
            TakeDamage = gameObject.AddComponent<IDamTakeDamage>();
            Level.Instance.Surface.BuildNavMesh();
        }
        
    }
    
}