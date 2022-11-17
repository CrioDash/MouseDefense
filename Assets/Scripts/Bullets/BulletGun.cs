using System.Collections;
using Enemies;
using TMPro;
using UI.Pause;
using UnityEngine;

namespace Bullets
{
    public class BulletGun: Bullet
    {
        public override IEnumerator Move()
        {
            Enemy target = Parent.GetTarget();
            while (true)
            {
                if (target == null)
                {
                    Destroy(gameObject);
                    yield break;
                }
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
                    BulletSpeed * Time.fixedDeltaTime);
                yield return null;
            }
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                StopCoroutine(Move());
                other.GetComponent<Enemy>().TakeDamage.TakeDamage(GetDmg(), DamageType.Normal);
                Destroy(gameObject);
            }
        }
    }
}