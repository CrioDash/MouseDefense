using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;

namespace Levels
{
    public class Level1 : Level
    {

        public override IEnumerator LevelScenario()
        {
            for(int i =0; i<5; i++)
            {
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                GameObject enemyGM = Instantiate(Enemies[0], enemyContainer.transform);
                enemyGM.transform.position = enemyContainer.transform.position;
                yield return new WaitForSeconds(2f);
            }
        }
    }
}