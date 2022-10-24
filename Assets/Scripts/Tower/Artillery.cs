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
            
        }
        
        
    }
}