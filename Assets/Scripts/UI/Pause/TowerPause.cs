using System;
using Towers.Interfaces;
using UnityEngine;

namespace UI.Pause
{
    public class TowerPause:  MonoBehaviour, IPausable
    {
        private Animator _animator;
        private Tower _tower;
        private ITowerShoot _towerShoot;
        private ITowerAnimation _towerAnimation;
        
        private void Awake()
        {
            _tower = GetComponent<Tower>();
            _animator = GetComponent<Animator>();
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
            if(_towerShoot==null)
                _towerShoot = GetComponent<ITowerShoot>();
            if (_towerAnimation == null)
                _towerAnimation = GetComponent<ITowerAnimation>();
            _tower.TowerShoot = PauseScript.IsPaused ? null : _towerShoot;
            _tower.TowerAnimation = PauseScript.IsPaused ? null : _towerAnimation;
            if (_animator != null) 
                _animator.speed = PauseScript.IsPaused ? 0 : 1;
        }
    }
}