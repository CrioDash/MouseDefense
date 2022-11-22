using System.Collections;
using System.Linq;
using UnityEngine;

namespace Enemies
{
    public class IDefaultMove:MonoBehaviour,IEnemyMove
    {

        public IEnumerator Move()
        {
            Enemy enemy = GetComponent<Enemy>();
            for(int i =1;i<enemy.Waypoints.Count; i++)
            {
                enemy.Destination = enemy.Waypoints[i].transform.position;
                yield return new WaitUntil(
                    () => Vector3.Distance(enemy.transform.position, enemy.Destination) <= 1f);
            }
            Level.currentLevel.TakeDamage(enemy.Damage);
            Destroy(enemy.gameObject);
        }
    }
}