using System.Collections;
using UnityEngine;

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