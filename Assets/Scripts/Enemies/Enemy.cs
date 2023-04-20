using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bullets;
using Events;
using Game;
using GameData;
using TMPro;
using UI.Pause;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    
    [RequireComponent(typeof(EnemyPause))]
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Enemy stats")] 
        public TextMeshProUGUI Text;
        public Sprite Icon;
        public EnemyMoveType moveType;
        public float MaxHealth;
        public int Reward;
        public int Damage;
        public float Speed;
        public Variables.EnemyType EnemyType;

        public List<Transform> Waypoints
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
        
        [HideInInspector]
        public NavMeshAgent Agent;

        public void CreateDamageText(float dmg)
        {
            
            TextMeshProUGUI text = Instantiate(Text, Level.Instance.CanvasText.transform).GetComponent<TextMeshProUGUI>();
            text.transform.position = transform.position;
            text.color = Color.Lerp(Color.green, Color.red, 1 - CurrentHealth / MaxHealth);
            Vector3 pos = text.transform.localPosition;
            pos.x += 50;
            text.transform.localPosition = pos;
            text.text = Mathf.Ceil(dmg).ToString();
        }
        
        
        private void OnEnable()
        {
            
            CurrentHealth = MaxHealth;
            if(Agent!=null)
                Agent.speed = Speed;
            transform.position = Level.Instance.enemyContainer.transform.position;
            SetStats();
            if(Move!=null)
                StartCoroutine(Move.Move());
        }

        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();
        }

        public virtual void Update()
        {
            if (transform.position.y <= -20)
            {
                CurrentHealth = 0;
                MoveToPool();
            }
        }

        public abstract void SetStats();
        

        public void MoveToPool()
        {
            if (CurrentHealth <= 0)
            {
                Level.Instance.ChangeMoney(Reward);
            }
            StopAllCoroutines();
            transform.position = Level.Instance.enemyContainer.transform.position;
            Level.Instance.EnemyPool.Add(EnemyType, this);
        }
        

        public void SetWaypoints(Transform[] waypoints)
        {
            Waypoints = waypoints.ToList();
        }
    }
}