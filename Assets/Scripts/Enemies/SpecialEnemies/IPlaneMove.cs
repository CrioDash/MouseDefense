using System.Collections;
using UnityEngine;

namespace Enemies.SpecialEnemies
{
    public class IPlaneMove:MonoBehaviour, IEnemyMove
    {
        private Enemy _enemy;
        public IEnumerator Move()
        {
            if(_enemy==null)
                _enemy = GetComponent<Enemy>();
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(130, 50, transform.position.z),
                    _enemy.Speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}