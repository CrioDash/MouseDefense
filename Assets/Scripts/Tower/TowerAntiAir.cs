using Towers.Default;
using UnityEngine;

namespace Towers
{
    public class TowerAntiAir:Tower
    {
        
        private void Start()
        {
            TowerShoot = gameObject.AddComponent<DefaultTowerShoot>();
            TowerAnimation = gameObject.AddComponent<DefaultTowerAnimation>();
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