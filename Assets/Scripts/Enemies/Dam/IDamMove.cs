using System.Collections;
using UI.Pause;
using UnityEngine;
using Utilities;

namespace Enemies.Dam
{
    public class IDamMove:MonoBehaviour, IEnemyMove
    {
        public IEnumerator Move()
        {
            yield break;
        }
    }
}