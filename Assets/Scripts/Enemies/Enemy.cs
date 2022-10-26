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
    public abstract class Enemy : MonoBehaviour
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
            set => Agent.SetDestination(value);
            get => Agent.destination;
        }

        public float CurrentHealth
        {
            set;
            get;
        }
        
        public IEnemyMove Move
        {
            set;
            get;
        }

        public ITakeDamage TakeDamage
        {
            set;
            get;
        }
        
        protected Rigidbody Body;
        protected NavMeshAgent Agent;
       

        private void Start()
        {
            Level = FindObjectOfType<Level>();
            CurrentHealth = MaxHealth;
            Agent.speed = Speed;
            SetStats();
            StartCoroutine(Move.Move());
        }

        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();
        }

        public abstract void SetStats();
        

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