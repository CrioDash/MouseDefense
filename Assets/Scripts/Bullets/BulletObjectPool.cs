using System.Collections.Generic;
using GameData;
using UnityEngine;
using TowerType = GameData.Variables.TowerType;

namespace Bullets
{
    public class BulletObjectPool:MonoBehaviour
    {

        private Dictionary<Variables.TowerType, Stack<Bullet>> _bullets = new Dictionary<TowerType, Stack<Bullet>>();

        public void Get(TowerType type, Tower tower)
        {
            if(_bullets.TryGetValue(type, out Stack<Bullet> stack))
            {
                if (stack.Count != 0)
                {
                    Bullet bullet = stack.Pop();
                    bullet.gameObject.SetActive(true);
                    bullet.SetStats(tower);
                }
                else
                {
                    Instantiate(tower.bulletPrefab, Level.Instance.bulletContainer.transform).GetComponent<Bullet>().SetStats(tower);
                }
            }
            else
            {
                _bullets.Add(type, new Stack<Bullet>());
                Instantiate(tower.bulletPrefab, Level.Instance.bulletContainer.transform).GetComponent<Bullet>().SetStats(tower);
            }
        }

        public void Add(TowerType type, Bullet bullet)
        {
            _bullets[type].Push(bullet);
            bullet.gameObject.SetActive(false);
        }
        

    }
}