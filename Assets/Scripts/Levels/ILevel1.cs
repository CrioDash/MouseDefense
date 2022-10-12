using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;

namespace Levels
{
    public class ILevel1 : Level
    {

        public override IEnumerator LevelScenario()
        {
            while (enemies.Count < 50)
            {
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
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