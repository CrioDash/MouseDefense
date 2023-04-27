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
            GetComponent<NavMeshAgent>().enabled = false;
            EnemyParatrooper enemy = GetComponent<EnemyParatrooper>();
            Rigidbody body = GetComponent<Rigidbody>();
            body.velocity = Vector3.zero;
            enemy.moveType = EnemyMoveType.Air;
            enemy.parashootHP = enemy.ParashootHP;
            enemy.parashoot.SetActive(true);
            enemy.SetWaypoints(Level.Instance.Waypoints.ToArray());
            yield return new WaitForSeconds(2);
            body.useGravity = true;
            yield return new WaitUntil(() => transform.position.y <= 0.5f);
            if (!enemy.parashoot.gameObject.activeSelf)
            {
                enemy.TakeDamage.TakeDamage(100, DamageType.Normal);
                yield break;
            }
            enemy.moveType = EnemyMoveType.Ground;
            enemy.parashoot.SetActive(false);
            enemy.parashootHP = 0;
            enemy.Agent.enabled = true;
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            collider.enabled = false;
            collider.enabled = true;
            for(int i =1;i<enemy.Waypoints.Count; i++)
            {
                if(!enemy.gameObject.activeSelf)
                    yield break;
                enemy.Destination = enemy.Waypoints[i].transform.position;
                yield return new WaitUntil(
                    () => Vector3.Distance(enemy.transform.position, enemy.Destination) <= 2f);
            }
            Level.Instance.TakeDamage(enemy.Damage);
            StartCoroutine(enemy.MoveToPool());
        }
        
    }
}