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
            bool Blood = _enemy.Blood;
            if (type == DamageType.Periodical)
                Blood = false;
            if (_parashoot==null && _enemy.ParashootHP > 0)
                _parashoot = transform.Find("Parashoot").gameObject;
            if (_enemy.parashootHP > 0)
            {
                _enemy.CreateDamageText(dmg, false);
                _enemy.parashootHP -= dmg;
            }
            if (_parashoot.gameObject.activeSelf && _enemy.parashootHP <= 0)
            {
                _enemy.GetComponent<Rigidbody>().AddForce(0, -1000, 0);
                _enemy.moveType = EnemyMoveType.None;
                _parashoot.gameObject.SetActive(false);
            }
            if (!_parashoot.gameObject.activeSelf)
            {
                _enemy.CurrentHealth -= dmg;
                _enemy.CreateDamageText(dmg, Blood);
                if (_enemy.CurrentHealth <= 0)
                     StartCoroutine(_enemy.MoveToPool());
            }
        }
    }
}