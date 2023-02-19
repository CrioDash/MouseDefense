using TMPro;
using UnityEngine;

namespace Enemies
{
    public class DefaultTakeDamage: MonoBehaviour, ITakeDamage
    {
        private Enemy _enemy;
        public void TakeDamage(float dmg, DamageType type)
        {
            if(_enemy == null)
                _enemy = GetComponent<Enemy>();
            _enemy.CurrentHealth-=dmg;
            _enemy.CreateDamageText(dmg);
            if (_enemy.CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}