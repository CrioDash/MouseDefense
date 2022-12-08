using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Dammer
{
    public class EnemyDammer : Enemy
    {
        public GameObject dam;
        public GameObject damPrefab;

        [HideInInspector] public List<Vector3> _damPoints = new List<Vector3>();
        
        public override void SetStats()
        {
            List<GameObject> points = new List<GameObject>();
            foreach (Transform transform in Level.currentLevel.DammerPositions.GetComponentsInChildren<Transform>())
            {
                points.Add(transform.gameObject);
            }
            points.Remove(points[0]);
            SetWaypoints(points.ToArray());
            foreach (Transform transform in Level.currentLevel.DamPositions.GetComponentsInChildren<Transform>())
            {
                _damPoints.Add(transform.position);
            }

            _damPoints.Remove(_damPoints[0]);
           
            Move = gameObject.AddComponent<IDammerMove>();
            TakeDamage = gameObject.AddComponent<IDefaultTakeDamage>();
        }
    }
}