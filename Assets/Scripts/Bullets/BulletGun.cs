using System;
using System.Collections;
using Enemies;
using TMPro;
using UI.Pause;
using UnityEngine;

namespace Bullets
{
    public class BulletGun : Bullet
    {
        private GameObject _target;

        private void Start()
        {
            _target = Parent.GetTarget().gameObject;
        }

        public override void Move()
        {
            if (_target==null)
            {
                Destroy(gameObject);
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position,
                BulletSpeed * Time.deltaTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _target.GetComponent<Enemy>().TakeDamage.TakeDamage(GetDmg(), DamageType.Normal);
                Destroy(gameObject);
            }
        }
    }
}