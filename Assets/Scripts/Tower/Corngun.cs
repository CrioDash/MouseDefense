using System;
using UI.Pause;
using UnityEngine;

namespace Towers
{
    public class Corngun:Tower
    {
        public GameObject Barrel;
        public float RotationSpeed;
        

       public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(PauseScript.IsPaused)
                return;
            if(GetTarget()!=null)
                Barrel.transform.Rotate(Vector3.right*(RotationSpeed*Time.fixedDeltaTime));
        }

        public override void Shoot()
        {
            if (GetTarget() == null)
                return;
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.position = bulletSpawn.transform.position;
        }

        public override void LevelUp()
        {
            transform.localScale += Vector3.one * 0.15f;
            bulletDamage *= 1.5f;
            bulletCooldown *= 0.8f;
            bulletSpeed *= 1.2f;
            attackRange += 0.5f;
        }
    }
}