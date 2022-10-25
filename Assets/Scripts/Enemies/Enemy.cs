using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bullets;
using Events;
using UI.Pause;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    
    [RequireComponent(typeof(EnemyPause))]
    public abstract class Enemy : MonoBehaviour, ITakeDamage
    {
        [Header("Enemy stats")] 
        public EnemyType Type;
        public float MaxHealth;
        public int Reward;
        public int Damage;
        public float Speed;

        public List<GameObject> Waypoints
        {
            set;
            get;
        }
        
        public Level Level
        {
            set;
            get;
        }

        public Vector3 Destination
        {
            set => _agent.SetDestination(value);
            get => _agent.destination;
        }
        
        protected float CurrentHealth;
        protected Rigidbody _body;
        protected NavMeshAgent _agent;
        protected IEnemyMove _move;

        private void Start()
        {
            Level = FindObjectOfType<Level>();
            CurrentHealth = MaxHealth;
            _agent.speed = Speed;
            SetStats();
            StartCoroutine(_move.Move(this));
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public abstract void SetStats();

        public virtual void TakeDamage(int dmg)
        {
            CurrentHealth-=dmg;
            if (CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }

        }

        private void OnDestroy()
        {
            Level.ChangeMoney(Reward);
        }

        public void SetWaypoints(GameObject[] waypoints)
        {
            Waypoints = waypoints.ToList();
        }
    }
}