using System.Collections;
using Enemies;
using UI.Pause;
using UnityEngine;

namespace Towers.TowerGuns
{
    public class BulletGun: Bullet
    {
        public override IEnumerator Move()
        {
            BaseEnemy target = _parent.GetTarget();
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
                    bulletSpeed * Time.fixedDeltaTime);
                yield return null;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                StopCoroutine(Move());
                BaseEnemy baseEnemy = other.GetComponent<BaseEnemy>();
                baseEnemy.TakeDamage(GetDmg(), this);
            }
        }
    }
}