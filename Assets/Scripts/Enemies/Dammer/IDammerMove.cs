using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

namespace Enemies.Dammer
{
    public class IDammerMove : MonoBehaviour, IEnemyMove
    {
        
        public IEnumerator Move()
        {
            EnemyDammer enemy = GetComponent<EnemyDammer>();
            WaitUntil wait = new WaitUntil(() => Vector3.Distance(enemy.transform.position, enemy.Destination) <= 2f);
            enemy.Destination = enemy.Waypoints[0].transform.position;
            yield return wait;
            GameObject gm = Instantiate(enemy.damPrefab, Level.currentLevel.Surface.transform);
            gm.transform.position = enemy._damPoints[0];
            Destroy(enemy.dam.gameObject);
            enemy.Destination = enemy.Waypoints[1].transform.position;
            yield return wait;
            Destroy(enemy.gameObject);
        }
    }
}