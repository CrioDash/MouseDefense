﻿using UnityEngine;

namespace Enemies.Shielder
{
    public class IShielderTakeDamage: MonoBehaviour, ITakeDamage
    {
        private EnemyShielder _shielder;
        private Rigidbody _body;
        public void TakeDamage(float dmg, DamageType type)
        {
            
            if (_body == null)
                _body = GetComponent<Rigidbody>();
            if (_shielder == null)
                _shielder = GetComponent<EnemyShielder>();
            bool Blood = _shielder.Blood;
            if (type == DamageType.Periodical)
                Blood = false;
            if (_shielder.shieldHP > 0)
            {
                Vector3 dir = _shielder.Shield.transform.position - transform.position;
                dir = -dir.normalized;
                Vector3 waypoint = _shielder.Agent.destination;
                if(type != DamageType.Periodical)
                    _shielder.Agent.Warp(transform.position + dir/2);
                _shielder.Agent.destination = waypoint;
                _shielder.shieldHP -= type == DamageType.Splash ? 2 : 1;
                _shielder.CreateDamageText(dmg, false);
                return;
            }
            if (_shielder.Shield.activeSelf)
                _shielder.Shield.SetActive(false);
            _shielder.CurrentHealth -= dmg;
            _shielder.CreateDamageText(dmg, Blood);
            if (_shielder.CurrentHealth <= 0)
               StartCoroutine(_shielder.MoveToPool());
        }
    }
}