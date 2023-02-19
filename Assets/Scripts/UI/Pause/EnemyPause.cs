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
        private Rigidbody _body;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _agent = GetComponent<NavMeshAgent>();
            _body = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            PauseScript.Pauses.Add(this);
        }
        
        private void OnDisable()
        {
            PauseScript.Pauses.Remove(this);
        }
        public void Pause()
        {
            if (PauseScript.IsPaused)
            {
                _body.isKinematic = true;
                if(_agent!=null)
                    _agent.speed = 0;
            }
            else
            {
                _body.isKinematic = false;
                if(_agent!=null)
                    _agent.speed = _enemy.Speed;
            }
        }
    }
}