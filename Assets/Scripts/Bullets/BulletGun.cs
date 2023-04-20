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
        

        public override IEnumerator Move()
        {
            while (true)
            {
                if (!_target.activeSelf)
                {
                    ReturnToPool();
                    yield break;
                }
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position,
                    BulletSpeed * Time.deltaTime);
                yield return null;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _target.GetComponent<Enemy>().TakeDamage.TakeDamage(GetDmg(), DamageType.Normal);
                ReturnToPool();
            }
        }
    }
}