using System;
using Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace UI.Pause
{
    public class EnemyPause: MonoBehaviour, IPausable
    {
        private NavMeshAgent _agent;
        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            PauseScript._pauses.Add(this);
        }
        
        private void OnDisable()
        {
            PauseScript._pauses.Remove(this);
        }
        public void Pause()
        {
            if (PauseScript.IsPaused)
            {
                _agent.speed = 0;
            }
            else
            {
                _agent.speed = _enemy.Speed;
            }
        }
    }
}