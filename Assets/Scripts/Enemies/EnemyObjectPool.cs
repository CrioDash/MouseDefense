using System.Collections.Generic;
using Bullets;
using GameData;
using UnityEngine;
using EnemyType = GameData.Variables.EnemyType;

namespace Enemies
{
    public class EnemyObjectPool : MonoBehaviour
    {
        private Dictionary<EnemyType, Stack<Enemy>> _enemies = new Dictionary<EnemyType, Stack<Enemy>>();
        
        
        public Enemy Get(EnemyType type)
        {
            if(_enemies.TryGetValue(type, out Stack<Enemy> stack))
            {
                if (stack.Count != 0)
                {
                    Enemy enemy = stack.Pop();
                    enemy.gameObject.SetActive(true);
                    return enemy;
                }
                else
                {
                    return Instantiate(Level.Instance.EnemyDict[type], Level.Instance.enemyContainer.transform).GetComponent<Enemy>();
                }
            }
            else
            {
                _enemies.Add(type, new Stack<Enemy>());
                return Instantiate(Level.Instance.EnemyDict[type], Level.Instance.enemyContainer.transform).GetComponent<Enemy>();
            }
        }

        public void Add(EnemyType type, Enemy enemy)
        {
            _enemies[type].Push(enemy);
            enemy.StopAllCoroutines();
            enemy.gameObject.SetActive(false);
        }
    }
}