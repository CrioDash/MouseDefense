using System.Collections;
using Enemies;
using UI.Pause;
using UnityEditor.AI;
using UnityEngine;

namespace Levels
{
    public class Level2: Level
    {
       


        public override IEnumerator LevelScenario()
        {
            for(int i =0; i<200; i++)
            {
                while (PauseScript.IsPaused)
                { 
                    yield return null;
                }
                GameObject enemyGameObject = Instantiate(Enemies[0], enemyContainer.transform);
                enemyGameObject.transform.position = enemyContainer.transform.position;
                Enemy enemy = enemyGameObject.GetComponentInChildren<Enemy>();
                enemy.SetWaypoints(Waypoints.ToArray());
                yield return new WaitForSeconds(0.75f);
            }
        }
    }
}