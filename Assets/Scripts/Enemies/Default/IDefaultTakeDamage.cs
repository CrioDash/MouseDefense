using UnityEngine;

namespace Enemies
{
    public class IDefaultTakeDamage: MonoBehaviour, ITakeDamage
    {
        private Enemy enemy;
        public void TakeDamage(int dmg, DamageType type)
        {
            if(enemy == null)
                enemy = GetComponent<Enemy>();
            enemy.CurrentHealth-=dmg;
            if (enemy.CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}