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
            Move = gameObject.AddComponent<IDamMove>();
            TakeDamage = gameObject.AddComponent<IDamTakeDamage>();
            _surface = FindObjectOfType<NavMeshSurface>();
        }

        private void OnEnable()
        {
            _surface.BuildNavMesh();
        }
        
        
    }
}