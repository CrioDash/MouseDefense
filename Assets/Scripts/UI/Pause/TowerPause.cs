using System;
using UnityEngine;

namespace UI.Pause
{
    public class TowerPause:  MonoBehaviour, IPausable
    {
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
            if (_animator != null) 
                _animator.speed = PauseScript.IsPaused ? 0 : 1;
        }
    }
}