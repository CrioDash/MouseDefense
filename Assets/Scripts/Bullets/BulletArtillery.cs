using System;
using System.Collections;
using Enemies;
using UI.Pause;
using UnityEngine;
using Utilities;

namespace Bullets
{
    public class BulletArtillery: Bullet
    {
        public GameObject radius;

        private Vector3 endPos;
        public override IEnumerator Move()
        {
            Vector3 startPos = transform.position;
            endPos = parent.GetTarget().transform.position;
            endPos.y -= 1;
            Vector3 middlePos1 = Vector3.Lerp(startPos, endPos, 1/3f);
            Vector3 middlePos2 = Vector3.Lerp(startPos, endPos, 2/3f);
            
            middlePos1.y += 6;
            middlePos2.y += 6;
            float time = 0;
            while (time<1)
            {
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }

                transform.position = Bezier.GetBezier(startPos, middlePos1, middlePos2, endPos, time);
                transform.rotation = Quaternion.LookRotation(Bezier.BezierRotation(startPos, middlePos1, middlePos2, endPos, time));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x+90f, transform.eulerAngles.y, 0);
                time += Time.deltaTime;
                yield return null;
            }
            yield break;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger enter");
            if (other.CompareTag("Road") || other.CompareTag("Enemy"))
            {
                Debug.Log("entered road");
                StopCoroutine(Move());
                GameObject rad = Instantiate(radius);
                rad.transform.position = endPos;
                Destroy(rad, 0.4f);
                
                Collider[] targets = Physics.OverlapSphere(transform.position, 3, 1<<7);
                Debug.Log(targets.Length);
                foreach (Collider col in targets)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        Debug.Log("dealed dmg");
                        col.GetComponent<Enemy>().TakeDamage(parent.bulletDamage, this);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}