using System.Collections;
using Enemies;
using UI.Pause;
using UnityEngine;

namespace Bullets
{
    public class BulletAA:Bullet
    {
        private GameObject _target;

        private void Start()
        {
            _target = Parent.GetTarget().gameObject;
        }
        
        public override void Move()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position,
                BulletSpeed * Time.deltaTime);
            transform.LookAt(_target.transform.position);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x+90f,  transform.eulerAngles.y, 0);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage.TakeDamage(GetDmg(), DamageType.Pierce);
                Destroy(gameObject);
            }
        }
    }
}