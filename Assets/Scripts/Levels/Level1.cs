using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;
using Variables = GameData.Variables;

namespace Levels
{
    public class Level1 : Level
    {

        public override IEnumerator LevelScenario()
        {
            ChangeMoney(225);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(WaveTextScript.Instance.TextMove(Variables.EnemyType.Shielder));
            yield return StartCoroutine(Wave(Variables.EnemyType.Shielder, 5, 2f));
            yield return new WaitUntil(() => enemyContainer.transform.childCount == 0);
            yield return StartCoroutine(WaveTextScript.Instance.TextMove(Variables.EnemyType.Tractor));
            yield return StartCoroutine(Wave(Variables.EnemyType.Tractor, 5, 3f));
            yield return new WaitUntil(() => enemyContainer.transform.childCount == 0);
            yield return StartCoroutine(WaveTextScript.Instance.TextMove(Variables.EnemyType.Parashoot));
            yield return StartCoroutine(Wave(Variables.EnemyType.Plane, 3, 1.5f));
        }
    }
}