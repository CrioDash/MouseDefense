using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bullets;
using Events;
using Game;
using TMPro;
using UI.Pause;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    
    [RequireComponent(typeof(EnemyPause))]
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Enemy stats")] 
        public TextMeshProUGUI Text;
        public EnemyType Type;
        public float MaxHealth;
        public int Reward;
        public int Damage;
        public float Speed;
        public Variables.EnemyType EnemyType;

        public List<GameObject> Waypoints
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

        public void CreateDamageText(int dmg)
        {
            TextMeshProUGUI text = Instantiate(Text, Level.currentLevel.Canvas.transform).GetComponent<TextMeshProUGUI>();
            text.transform.position = transform.position;
            Vector3 pos = text.transform.localPosition;
            pos.x += 50;
            text.transform.localPosition = pos;
            text.text = dmg.ToString();
        }
        
        private void Start()
        {
            CurrentHealth = MaxHealth;
            if(Agent!=null)
                Agent.speed = Speed;
            SetStats();
            if(Move!=null)
                StartCoroutine(Move.Move());
        }

        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();
        }

        public virtual void FixedUpdate()
        {
            if (transform.position.y <= -20)
            {
                CurrentHealth = 0;
                Destroy(gameObject);
            }
        }

        public abstract void SetStats();
        

        private void OnDestroy()
        {
            if (CurrentHealth <= 0)
            {
                Level.currentLevel.ChangeMoney(Reward);
            }
        }
        

        public void SetWaypoints(GameObject[] waypoints)
        {
            Waypoints = waypoints.ToList();
        }
    }
}