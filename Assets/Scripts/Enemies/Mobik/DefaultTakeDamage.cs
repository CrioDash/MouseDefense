using TMPro;
using UnityEngine;

namespace Enemies
{
    public class DefaultTakeDamage: MonoBehaviour, ITakeDamage
    {
        private Enemy _enemy;
        public void TakeDamage(float dmg, DamageType type)
        {
            if(_enemy==null)
                _enemy = GetComponent<Enemy>();
            bool Blood = _enemy.Blood;
            if (type == DamageType.Periodical)
                Blood = false;
            _enemy.CurrentHealth-=dmg;
            _enemy.CreateDamageText(dmg, Blood);
            if (_enemy.CurrentHealth <= 0)
            {
               StartCoroutine(_enemy.MoveToPool());
            }
        }
    }
}