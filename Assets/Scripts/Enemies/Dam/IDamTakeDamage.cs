using UnityEngine;

namespace Enemies.SpecialEnemies
{
    public class IDamTakeDamage:MonoBehaviour, ITakeDamage
    {
        private Enemy enemy;
        public void TakeDamage(int dmg, DamageType type)
        {
            if(enemy == null)
                enemy = GetComponent<Enemy>();
            enemy.CurrentHealth-=dmg;
            enemy.StartCoroutine(enemy.Move.Move());
        }
    }
}