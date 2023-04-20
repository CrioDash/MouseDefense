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
        public override IEnumerator InstantiatePrefabs()
        {
            yield break;
        }

        public override IEnumerator LevelScenario()
        {
            ChangeMoney(225);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(WaveTextScript.Instance.TextMove(Variables.EnemyType.Parashoot));
            StartCoroutine(Wave(Variables.EnemyType.Plane, 50, 2f));
            //yield return new WaitUntil(() => enemyContainer.transform.childCount == 0);
            yield return StartCoroutine(WaveTextScript.Instance.TextMove(Variables.EnemyType.Tractor));
            StartCoroutine(Wave(Variables.EnemyType.Tractor, 20, 3f));
            //yield return new WaitUntil(() => enemyContainer.transform.childCount == 0);
            yield return StartCoroutine(WaveTextScript.Instance.TextMove(Variables.EnemyType.Shielder));
            StartCoroutine(Wave(Variables.EnemyType.Shielder, 30, 2f));
            yield return StartCoroutine(WaveTextScript.Instance.TextMove(Variables.EnemyType.Mobik));
            StartCoroutine(Wave(Variables.EnemyType.Lighter, 50, 2f));
        }
    }
}