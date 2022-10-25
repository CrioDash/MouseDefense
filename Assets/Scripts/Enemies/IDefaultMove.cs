using System.Collections;
using System.Linq;
using UnityEngine;

namespace Enemies
{
    public class IDefaultMove:MonoBehaviour,IEnemyMove
    {

        public IEnumerator Move(Enemy enemy)
        {
            
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