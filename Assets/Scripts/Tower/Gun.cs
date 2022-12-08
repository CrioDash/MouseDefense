using System;
using System.Collections;
using UI.Pause;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Towers
{
    public class Gun:Tower
    {

        public override void Shoot()
        {
            
            if (GetTarget() == null)
                return;
            _animator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.position = bulletSpawn.transform.position;
        }

        public override void LevelUp()
        {
            transform.localScale += Vector3.one * 0.15f;
            bulletDamage = (int)(bulletDamage * 1.35f);
            bulletCooldown *= 0.8f;
            bulletSpeed *= 1.2f;
            attackRange += 0.5f;
        }
        
        
    }
}