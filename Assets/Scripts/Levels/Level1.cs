﻿using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;
using Variables = Game.Variables;

namespace Levels
{
    public class Level1 : Level
    {

        public override IEnumerator LevelScenario()
        {
            ChangeMoney(225);
            yield return StartCoroutine(Wait(1f));
            yield return StartCoroutine(Wave(Variables.EnemyType.Mobik, 10, 1.5f));
            yield return StartCoroutine(Wait(30f));
            yield return StartCoroutine(Wave(Variables.EnemyType.Tractor, 3, 2f));
            yield return StartCoroutine(Wait(10f));
            yield return StartCoroutine(Wave(Variables.EnemyType.Plane, 3, 1.5f));
        }
    }
}