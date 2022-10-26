using UnityEngine;

namespace Enemies
{
    public class IParatrooperTakeDamage: MonoBehaviour, ITakeDamage
    {
        private GameObject _parashoot; 
        private EnemyParatrooper _enemy;
        public void TakeDamage(int dmg, DamageType type)
        {
           if(_enemy==null) 
               _enemy = GetComponent<EnemyParatrooper>();
           if (_parashoot == null && _enemy.ParashootHP > 0)
               _parashoot = transform.Find("Parashoot").gameObject;
           if (_enemy.ParashootHP > 0)
               _enemy.ParashootHP -= dmg;
           if (_parashoot != null && _enemy.ParashootHP <= 0)
           {
               _enemy.GetComponent<Rigidbody>().useGravity = true;
               _enemy.Type = EnemyType.Ground;
               Destroy(_parashoot.gameObject);
           }
           if (_parashoot == null)
           {
               _enemy.CurrentHealth -= dmg;
               if (_enemy.CurrentHealth <= 0)
                   Destroy(_enemy.gameObject);
           }
        }
    }
}