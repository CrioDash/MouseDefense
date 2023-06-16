using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bullets;
using Events;
using Game;
using GameData;
using TMPro;
using UI;
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
        public Sprite Icon;
        public EnemyMoveType moveType;
        public float MaxHealth;
        public int Reward;
        public int Damage;
        public float Speed;
        public Variables.EnemyType EnemyType;
        public bool Blood;

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

        public void CreateDamageText(float dmg, bool blood)
        {

            TextMeshProUGUI text = Level.Instance.TextPool.Get();
            if(blood) 
                Level.Instance.ParticlePool.Get(transform);
            text.transform.position = transform.position;
            text.transform.Translate(Vector3.back * 30f);
            text.color = Color.Lerp(Color.green, Color.red, 1 - CurrentHealth / MaxHealth);
            Vector3 pos = text.transform.localPosition;
            pos.x += 50;
            text.transform.localPosition = pos;
            text.text = (Mathf.Ceil(dmg)*10).ToString();
        }
        
        
        private void OnEnable()
        {
            
            CurrentHealth = MaxHealth;
            transform.position = Level.Instance.enemyContainer.transform.position;
            if (Agent != null)
            {
                Agent.enabled = true;
                Agent.speed = Speed;
            }
           
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
                StartCoroutine(MoveToPool());
            }
        }

        public abstract void SetStats();
        

        public IEnumerator MoveToPool()
        {
            if (CurrentHealth <= 0)
            {
                Level.Instance.ChangeMoney(Reward);
            }
            if(Agent!=null) 
                Agent.enabled = false;
            if(Move!=null)
                StopCoroutine(Move.Move());
            transform.position = Vector3.left*200;
            yield return new WaitForSeconds(0.5f);
            Level.Instance.EnemyPool.Add(EnemyType, this);
        }
        

        public void SetWaypoints(Transform[] waypoints)
        {
            Waypoints = waypoints.ToList();
        }
    }
}