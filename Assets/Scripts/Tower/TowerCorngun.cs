using System;
using Towers.Corngun;
using Towers.Default;
using UI.Pause;
using UnityEngine;

namespace Towers
{
    public class TowerCorngun:Tower
    {
        public GameObject Barrel;
        public float RotationSpeed;
        
        private void Start()
        {
            TowerShoot = gameObject.AddComponent<DefaultTowerShoot>();
            TowerAnimation = gameObject.AddComponent<CorngunTowerAnimation>();
        }

        public override void LevelUp()
        {
            transform.localScale += Vector3.one * 0.15f;
            bulletDamage *= 1.5f;
            bulletCooldown *= 0.8f;
            bulletSpeed *= 1.2f;
            attackRange += 0.5f;
            radiusSprite.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 1);
        }
    }
}