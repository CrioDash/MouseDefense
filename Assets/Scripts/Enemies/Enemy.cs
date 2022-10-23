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

        protected float CurrentHealth;
        protected Rigidbody _body;
        protected NavMeshAgent _agent;
        protected GameObject[] _waypoints;
        protected Level _level;

        private void Start()
        {
            _level = FindObjectOfType<Level>();
            CurrentHealth = MaxHealth;
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public virtual void TakeDamage(int dmg, Bullet proj)
        {
            CurrentHealth-=dmg;
            if (CurrentHealth <= 0)
            {
                _level.ChangeMoney(Reward);
                proj.parent.FindTarget();
                Destroy(gameObject);
            }
            StartCoroutine(VisualTakeDamage(dmg));
            
            Destroy(proj.gameObject);
        }

        

        public IEnumerator VisualTakeDamage(int dmg)
        {
            yield return new WaitUntil(() => CurrentHealth != MaxHealth);
            float t = 0;
            while (t<1)
            {
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                t += Time.deltaTime * 8;
                yield return null;
            }
        }
        
        public void SetWaypoints(GameObject[] waypoints)
        {
            _agent.speed = Speed;
            _waypoints = waypoints;
            _agent.SetDestination(_waypoints[1].transform.position);
            StartCoroutine(MoveDestination());
        }

        public virtual IEnumerator MoveDestination()
        {
            GameObject currentWaypoint = _waypoints[1];

            yield return new WaitUntil(
                () => Vector3.Distance(transform.position, currentWaypoint.transform.position) <= 0.7f);
            if(currentWaypoint.name == _waypoints[_waypoints.Length-1].name)
            {
                _level.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
        
        
        
    }
}