using System.Collections;
using Enemies;
using Game;
using UI.Pause;
using UnityEngine;

namespace Levels
{
    public class Level2: Level
    {
        
        public override IEnumerator LevelScenario()
        {
            ChangeMoney(200);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(Wave(Variables.EnemyType.Dammer, 1, 0.5f));
            yield return StartCoroutine(Wave(Variables.EnemyType.Mobik, 10, 1.5f));
        }
    }
}