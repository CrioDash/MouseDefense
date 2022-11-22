using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Pause;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.SpecialEnemies
{
    public class Plane:MonoBehaviour
    {
        public GameObject paratroop;
        public float speed;

        private Vector3 target;
        private void Awake()
        {
            
        }

        private void Start()
        {
            target = CalculatePoints();
            Debug.Log(target);
            transform.position = new Vector3(-60, 60, target.z);
            StartCoroutine(WaitDesant());
        }

        private void Update()
        {
            if(PauseScript.IsPaused)
                return;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(60, 60, transform.position.z),
                speed * Time.fixedDeltaTime);
        }

        public IEnumerator WaitDesant()
        {
            yield return new WaitUntil(() => transform.position.x >= target.x);
            StartCoroutine(SpawnDesant(3, 1f));
            yield return new WaitUntil(() => transform.localPosition.x >= 60);
            Destroy(gameObject);
        }

        private IEnumerator SpawnDesant(int count, float time)
        {
            Vector3 spawnPoint = transform.position;
            for (int i = 0; i < count; i++)
            {
                GameObject gm = Instantiate(paratroop);
                gm.transform.position = spawnPoint;
                yield return new WaitForSeconds(time);
            }
        }

        Vector3 CalculatePoints()
        {
            List<Vector3> points = new List<Vector3>();
            for (int z = -30; z < 35; z++)
            {
                for (int x = -35; x < 5; x++)
                {
                    Physics.Raycast(new Vector3(x, 60, z), new Vector3(x, 0, z) - new Vector3(x, 60, z), out var raycast, Mathf.Infinity);
                    if (raycast.collider.CompareTag("Road"))
                        if (!Physics.OverlapSphere(new Vector3(x, 0, z), 1.5f).ToList()
                                .Exists(x => x.CompareTag("TowerPlace")))
                            points.Add(new Vector3(x, 60, z));
                }
            }
            return points[Random.Range(0, points.Count - 1)];
        }
    }
}