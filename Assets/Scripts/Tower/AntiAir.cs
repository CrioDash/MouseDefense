﻿using UnityEngine;

namespace Towers
{
    public class AntiAir:Tower
    {
        public override void Shoot()
        {
            if (GetTarget() == null)
                return;
            animator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.position = bulletSpawn.transform.position;
            
        }

        public override void LevelUp()
        {
            transform.localScale += Vector3.one * 0.15f;
            bulletDamage++;
            bulletCooldown *= 0.8f;
            bulletSpeed *= 1.2f;
            attackRange += 0.5f;
        }
    }
}