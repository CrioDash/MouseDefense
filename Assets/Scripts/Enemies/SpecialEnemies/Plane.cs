using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameData;
using UI.Pause;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.SpecialEnemies
{
    public class Plane:Enemy
    {
        public GameObject paratroop;

        private Vector3 target;

        public override void SetStats()
        {
            target = CalculatePoints();
            transform.position = new Vector3(-60, 50, target.z);
            StartCoroutine(WaitDesant());
        }
        

        public override void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(130, 50, transform.position.z),
                Speed * Time.deltaTime);
        }

        public IEnumerator WaitDesant()
        {
            yield return new WaitUntil(() => transform.position.x >= target.x);
            StartCoroutine(SpawnDesant(Random.Range(1,3), 2f));
            yield return new WaitUntil(() => transform.localPosition.x >= 130);
            MoveToPool();
        }

        private IEnumerator SpawnDesant(int count, float time)
        {
            Vector3 spawnPoint = transform.position;
            WaitForSeconds wait = new WaitForSeconds(time);
            for (int i = 0; i < count; i++)
            { 
                GameObject paratroop = Level.Instance.EnemyPool.Get(Variables.EnemyType.Parashoot).gameObject;
                paratroop.transform.position = spawnPoint;
                yield return wait;
            }
        }

        Vector3 CalculatePoints()
        {
            List<Vector3> points = new List<Vector3>();
            for (int z = -30; z < 35; z++)
            {
                for (int x = -35; x < 10; x++)
                {
                    Physics.Raycast(new Vector3(x, 50, z), new Vector3(x, 0, z) - new Vector3(x, 50, z), out var raycast, Mathf.Infinity);
                    if (raycast.collider.CompareTag("Road"))
                        if (!Physics.OverlapSphere(new Vector3(x, 0, z), 1.5f).ToList()
                                .Exists(x => x.CompareTag("TowerPlace")))
                            points.Add(new Vector3(x, 50, z));
                }
            }

            Vector3 random = points[Random.Range(0, points.Count - 1)];
            Debug.Log(random);
            return random;
        }
    }
}