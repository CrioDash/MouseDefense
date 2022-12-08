using System.Collections;
using Enemies;
using UI.Pause;
using UnityEngine;

namespace Bullets
{
    public class BulletAA:Bullet
    {
        public void FixedUpdate()
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
            transform.LookAt(Parent.GetTarget().transform.position);
            var eulerAngles = transform.eulerAngles;
            eulerAngles = new Vector3(eulerAngles.x+90f, eulerAngles.y, 0);
            transform.eulerAngles = eulerAngles;
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