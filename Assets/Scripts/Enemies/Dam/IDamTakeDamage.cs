using System.Collections.Generic;
using System.Linq;
using Enemies.Dam;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.SpecialEnemies
{
    public class IDamTakeDamage:MonoBehaviour, ITakeDamage
    {
        private Enemy _enemy;
        public void TakeDamage(float dmg, DamageType type)
        {
            if (type != DamageType.Splash)
                dmg /= 2;
            if(_enemy==null)
                _enemy = GetComponent<Enemy>();
            _enemy.CurrentHealth-=dmg;
            _enemy.CreateDamageText(dmg);
            if (_enemy.CurrentHealth <= 0)
            {
                Rigidbody body = _enemy.GetComponent<Rigidbody>();
                body.AddForce(0, -100, 0);
                body.useGravity = true;
                tag = "Untagged";
                _enemy.GetComponent<BoxCollider>().isTrigger = false;
                List<Collider> targets = Physics.OverlapBox(transform.position, new Vector3(9/2,9/2,9/2), 
                    Quaternion.identity,1<<7).ToList();
                targets.Remove(GetComponent<Collider>());
                foreach (Collider col in targets)
                {
                    col.GetComponent<Collider>().isTrigger = false;
                    col.GetComponent<NavMeshAgent>().enabled = false;
                }

                transform.parent = null;
                Level.currentLevel.Surface.BuildNavMesh();
            }
        }
    }
}