using System.Collections;
using Enemies;
using UI.Pause;
using UnityEngine;

namespace Bullets
{
    public class BulletAA:Bullet
    {

        public override IEnumerator Move()
        {
            while (true)
            {
                if (!_target.activeSelf || _target.transform.position.x <-100)
                {
                    ReturnToPool();
                    yield break;
                }
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position,
                    BulletSpeed * Time.deltaTime);
                transform.LookAt(_target.transform.position);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90f, transform.eulerAngles.y, 0);
                yield return null;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _target.GetComponent<Enemy>().TakeDamage.TakeDamage(GetDmg(), DamageType.Normal);
                ReturnToPool();
            }
        }
    }
}