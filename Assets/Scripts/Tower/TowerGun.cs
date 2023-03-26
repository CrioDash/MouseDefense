using System;
using System.Collections;
using Towers.Default;
using UI.Pause;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Towers
{
    public class TowerGun:Tower
    {
        private void Start()
        {
            TowerShoot = gameObject.AddComponent<DefaultTowerShoot>();
            TowerAnimation = gameObject.AddComponent<DefaultTowerAnimation>();
        }

        public override void LevelUp()
        {
            transform.localScale += Vector3.one * 0.15f;
            bulletDamage = (int)(bulletDamage * 1.35f);
            bulletCooldown *= 0.8f;
            bulletSpeed *= 1.2f;
            attackRange += 0.5f;
            radiusSprite.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 1);
            _detector.UpdateColliders();
            Debug.Log(attackRange);
        }
    }
}