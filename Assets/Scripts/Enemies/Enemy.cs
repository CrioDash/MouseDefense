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
        public Sprite Icon;
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
        
        [HideInInspector]
        public NavMeshAgent Agent;

        public void CreateDamageText(float dmg)
        {
            TextMeshProUGUI text = Instantiate(Text, Level.currentLevel.Canvas.transform).GetComponent<TextMeshProUGUI>();
            text.transform.position = transform.position;
            text.color = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(1 - CurrentHealth / MaxHealth - 0.1f));
            Vector3 pos = text.transform.localPosition;
            pos.x += 50;
            text.transform.localPosition = pos;
            text.text = Mathf.Ceil(dmg).ToString();
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

        public virtual void Update()
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