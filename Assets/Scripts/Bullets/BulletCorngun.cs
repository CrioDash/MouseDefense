using Enemies;
using UI.Pause;
using UnityEngine;

namespace Bullets
{
    public class BulletCorngun:Bullet
    {
        public void Update()
        {
            if (PauseScript.IsPaused)
            {
                return;
            }
            if (Parent.GetTarget() == null)
            {
                Destroy(gameObject);
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, Parent.GetTarget().transform.position,
                BulletSpeed * Time.fixedDeltaTime);
            
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage.TakeDamage(GetDmg(), DamageType.Normal);
                Destroy(gameObject);
            }
        }
    }
}