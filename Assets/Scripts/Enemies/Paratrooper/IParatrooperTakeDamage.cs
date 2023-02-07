using UnityEngine;

namespace Enemies
{
    public class IParatrooperTakeDamage: MonoBehaviour, ITakeDamage
    {
        private GameObject _parashoot; 
        private EnemyParatrooper _enemy;
        public void TakeDamage(float dmg, DamageType type)
        {
            if(_enemy==null) 
               _enemy = GetComponent<EnemyParatrooper>();
            if (_parashoot == null && _enemy.ParashootHP > 0)
                _parashoot = transform.Find("Parashoot").gameObject;
            if (_enemy.ParashootHP > 0)
            {
                _enemy.ParashootHP -= dmg;
                _enemy.CreateDamageText(dmg);
            }
            if (_parashoot != null && _enemy.ParashootHP <= 0)
            {
                _enemy.GetComponent<Rigidbody>().useGravity = true;
                _enemy.GetComponent<Rigidbody>().AddForce(0, -350, 0);
                _enemy.Type = EnemyType.Ground;
                Destroy(_parashoot.gameObject);
            }
            if (_parashoot == null)
            {
                _enemy.CreateDamageText(dmg);
                _enemy.CurrentHealth -= dmg;
                _enemy.CreateDamageText(dmg);
                if (_enemy.CurrentHealth <= 0)
                    Destroy(_enemy.gameObject);
            }
        }
    }
}