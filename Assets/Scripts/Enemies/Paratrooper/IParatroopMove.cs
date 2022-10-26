using System.Collections;
using System.Linq;
using Bullets;
using UI.Pause;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class IParatroopMove: MonoBehaviour, IEnemyMove
    {
        public IEnumerator Move()
        {
            EnemyParatrooper enemy = GetComponent<EnemyParatrooper>();
            GameObject parashoot = enemy.transform.Find("Parashoot").gameObject;
            enemy.SetWaypoints(enemy.Level.Waypoints.ToArray());
            yield return new WaitForSeconds(2);
            while (enemy.transform.position.y > 0.5)
            {
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                enemy.transform.position += Physics.gravity * Time.deltaTime/4;
                yield return null;
            }

            if (parashoot == null)
                enemy.TakeDamage.TakeDamage(100, DamageType.Normal);
            enemy.Type = EnemyType.Ground;
            Destroy(parashoot);
            enemy.ParashootHP = 0;
            enemy.GetComponent<NavMeshAgent>().enabled = true;
            enemy.GetComponent<CapsuleCollider>().enabled = false;
            enemy.GetComponent<CapsuleCollider>().enabled = true;
            for(int i =1;i<enemy.Waypoints.Count; i++)
            {
                enemy.Destination = enemy.Waypoints[i].transform.position;
                yield return new WaitUntil(
                    () => Vector3.Distance(enemy.transform.position, enemy.Destination) <= 0.96f);
            }
            enemy.Level.TakeDamage(enemy.Damage);
            Destroy(enemy);
        }
    }
}