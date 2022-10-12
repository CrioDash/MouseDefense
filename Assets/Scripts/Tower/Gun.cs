using System.Collections;
using UI.Pause;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Towers
{
    public class Gun:Tower
    {
        public GameObject head;
        
        public override void Shoot()
        {
            
            if (GetTarget() == null)
                return;
            _animator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.position = bulletSpawn.transform.position;
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
                head.transform.LookAt(GetTarget().transform.position);
                head.transform.eulerAngles = new Vector3(0, head.transform.eulerAngles.y + 90f, 0);
                yield return null;
            }
        }
        
    }
}