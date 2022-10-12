using System.Collections;
using Enemies;
using UnityEngine;

namespace Levels
{
    public class Level2: Level
    {
        //public override IEnumerator LevelScenario()
        //{
        //    yield break;
        //}
        
        public override IEnumerator LevelScenario()
        {
            while (enemies.Count < 50)
            {
                //while (PauseScript.IsPaused)
                //{
                //    yield return null;
                //}
                GameObject enemyGameObject = Instantiate(Enemies[0], enemyContainer.transform);
                enemies.Add(enemyGameObject);
                enemyGameObject.transform.position = enemyContainer.transform.position;
                Enemy enemy = enemyGameObject.GetComponentInChildren<Enemy>();
                enemy.SetWaypoints(Waypoints.ToArray());
                yield return new WaitForSeconds(0.75f);
            }
        }
    }
}