using System.Collections;
using UI.Pause;
using UnityEngine;

namespace Towers
{
    public class Artillery: Tower
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
            bulletDamage = (int)(bulletDamage*1.5f);
            bulletCooldown *= 0.8f;
            bulletSpeed *= 1.2f;
            attackRange += 0.5f;
        }
        
        
    }
}