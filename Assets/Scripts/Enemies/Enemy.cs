using System;
using System.Collections;
using Bullets;
using Events;
using UI.Pause;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    
    [RequireComponent(typeof(EnemyPause))]
    public class Enemy : MonoBehaviour, ITakeDamage
    {
        [Header("Enemy stats")] 
        public EnemyType Type;
        public float MaxHealth;
        public int Reward;
        public int Damage;
        public float Speed;
        public GameObject CurrentHealthGM;
        public GameObject HPBar;

        protected float CurrentHealth;
        protected Material _material;
        protected NavMeshAgent _agent;
        protected GameObject[] _waypoints;
        protected Level _level;

        private void Start()
        {
            _level = FindObjectOfType<Level>();
            CurrentHealth = MaxHealth;
            StartCoroutine(RotateHp());
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _material = GetComponent<Renderer>().sharedMaterial;
        }

        public virtual void TakeDamage(int dmg, Bullet proj)
        {
            CurrentHealth-=dmg;
            StartCoroutine(VisualTakeDamage(dmg));
            if (CurrentHealth <= 0)
            {
                proj.GetParent().FindTarget();
                _level.ChangeMoney(Reward);
                Destroy(gameObject);
            }
            Destroy(proj.gameObject);
        }

        public IEnumerator RotateHp()
        {
            while (true)
            {
                HPBar.transform.eulerAngles = new Vector3(45, -45, 0);
                yield return null;
            }
        }

        public IEnumerator VisualTakeDamage(int dmg)
        {
            while (true)
            {
                yield return new WaitUntil(() => CurrentHealth != MaxHealth);
                float t = 0;
                Vector3 scale = CurrentHealthGM.transform.localScale;
                while (t<1)
                {
                    while (PauseScript.IsPaused)
                    {
                        yield return null;
                    }
                    CurrentHealthGM.transform.localScale = new Vector3(Mathf.Lerp(scale.x,
                        scale.x * (CurrentHealth / MaxHealth), t), scale.y, scale.z);
                    t += Time.deltaTime * 8;
                    yield return null;
                }
            }
        }
        
        public void SetWaypoints(GameObject[] waypoints)
        {
            _agent.speed = Speed;
            _waypoints = waypoints;
            _agent.SetDestination(_waypoints[0].transform.position);
            StartCoroutine(MoveDestination());
        }

        public virtual IEnumerator MoveDestination()
        {
            GameObject currentWaypoint = _waypoints[0];
            while (currentWaypoint.name != _waypoints[_waypoints.Length-1].name)
            {
                yield return new WaitUntil(
                    () => Vector3.Distance(transform.position, currentWaypoint.transform.position) <= 0.7f);
                currentWaypoint = _waypoints[Array.IndexOf(_waypoints, currentWaypoint) + 1];
                _agent.SetDestination(currentWaypoint.transform.position);
            }

            yield return new WaitUntil(
                () => Vector3.Distance(transform.position, currentWaypoint.transform.position) <= 0.7f);
            _level.TakeDamage(Damage);
            Destroy(gameObject);
        }
        
        
        
    }
}