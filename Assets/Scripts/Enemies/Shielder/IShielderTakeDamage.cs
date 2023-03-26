using UnityEngine;

namespace Enemies.Shielder
{
    public class IShielderTakeDamage: MonoBehaviour, ITakeDamage
    {
        private EnemyShielder _shielder;
        private Rigidbody _body;
        public void TakeDamage(float dmg, DamageType type)
        {
            if (_body == null)
                _body = GetComponent<Rigidbody>();
            if (_shielder == null)
                _shielder = GetComponent<EnemyShielder>();
            if (_shielder.ShieldHP > 0)
            {
                Vector3 dir = _shielder.Shield.transform.position - transform.position;
                dir = -dir.normalized;
                Vector3 waypoint = _shielder.Agent.destination;
                _shielder.Agent.Warp(transform.position + dir/2);
                _shielder.Agent.destination = waypoint;
                _shielder.ShieldHP-=(int)type+1;
                return;
            }
            if (_shielder.Shield!=null)
                Destroy(_shielder.Shield.gameObject);
            _shielder.CurrentHealth -= dmg;
            _shielder.CreateDamageText(dmg);
            if (_shielder.CurrentHealth <= 0)
                Destroy(_shielder.gameObject);
        }
    }
}