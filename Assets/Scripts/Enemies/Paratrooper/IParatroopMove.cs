using System.Collections;
using UI.Pause;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Paratrooper
{
    public class IParatroopMove: MonoBehaviour, IEnemyMove
    {
        
        public IEnumerator Move()
        { 
            EnemyParatrooper enemy = GetComponent<EnemyParatrooper>();
            
            enemy.SetWaypoints(Level.currentLevel.Waypoints.ToArray());
            yield return new WaitForSeconds(2);
            while (transform.position.y > 0.5)
            {
                while (PauseScript.IsPaused)
                    yield return null;
                enemy.transform.position += Physics.gravity/4;
                yield return null;
            }
            if (enemy.parashoot == null)
                enemy.TakeDamage.TakeDamage(100, DamageType.Normal);
            enemy.Type = EnemyType.Ground;
            Destroy(enemy.parashoot);
            enemy.ParashootHP = 0;
            GetComponent<NavMeshAgent>().enabled = true;
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            collider.enabled = false;
            collider.enabled = true;
            for(int i =1;i<enemy.Waypoints.Count; i++)
            {
                enemy.Destination = enemy.Waypoints[i].transform.position;
                yield return new WaitUntil(
                    () => Vector3.Distance(enemy.transform.position, enemy.Destination) <= 0.96f);
            }
            Level.currentLevel.TakeDamage(enemy.Damage);
            Destroy(enemy.gameObject);
        }
        
    }
}