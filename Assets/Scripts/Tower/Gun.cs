using System.Collections;
using Towers.TowerGuns;
using UI.Pause;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Towers
{
    public class Gun:Tower
    {
        public GameObject Head;
        
        public override void Shoot()
        {
            
            if (GetTarget() == null)
                return;
            _animator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(BulletPrefab, transform);
            bullet.transform.position = BulletSpawn.transform.position;
        }

        public override void LevelUp()
        {
            
        }

        public override IEnumerator RotateHead()
        {
            while (true)
            {
                if (GetTarget() == null)
                {
                    yield return null;
                    continue;
                }
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                Head.transform.LookAt(GetTarget().transform.position);
                Head.transform.eulerAngles = new Vector3(0, Head.transform.eulerAngles.y + 90f, 0);
                yield return null;
            }
        }
        
    }
}