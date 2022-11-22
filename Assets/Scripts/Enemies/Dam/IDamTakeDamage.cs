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
        private Enemy enemy;
        public void TakeDamage(int dmg, DamageType type)
        {
            if (type != DamageType.Splash)
                dmg /= 5;
            if(enemy == null)
                enemy = GetComponent<Enemy>();
            enemy.CurrentHealth-=dmg;
            if (enemy.CurrentHealth <= 0)
            {
                Rigidbody body = enemy.GetComponent<Rigidbody>();
                body.AddForce(0, -100, 0);
                body.useGravity = true;
                enemy.GetComponent<BoxCollider>().isTrigger = false;
                List<Collider> targets = Physics.OverlapBox(transform.position, new Vector3(9/2,9/2,9/2), 
                    Quaternion.identity,1<<7).ToList();
                targets.Remove(GetComponent<Collider>());
                foreach (Collider col in targets)
                {
                    col.GetComponent<Collider>().isTrigger = false;
                    col.GetComponent<NavMeshAgent>().enabled = false;
                }
                NavMeshSurface surface = FindObjectOfType<NavMeshSurface>();
                surface.collectObjects = CollectObjects.Children;
                surface.BuildNavMesh();
            }
        }
    }
}