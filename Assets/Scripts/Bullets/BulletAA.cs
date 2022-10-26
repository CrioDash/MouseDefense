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
                transform.LookAt(Parent.GetTarget().transform.position);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x+90f, transform.eulerAngles.y, 0);
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