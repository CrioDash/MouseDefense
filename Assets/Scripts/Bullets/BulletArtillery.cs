using System.Collections;
using UI.Pause;
using UnityEngine;
using Utilities;

namespace Bullets
{
    public class BulletArtillery: Bullet
    {
        public override IEnumerator Move()
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = _parent.GetTarget().transform.position;
            endPos.y -= 1;
            Vector3 middlePos1 = Vector3.Lerp(startPos, endPos, 1/3f);
            Vector3 middlePos2 = Vector3.Lerp(startPos, endPos, 2/3f);
            
            middlePos1.y += 6;
            middlePos2.y += 6;
            float time = 0;
            while (time<1)
            {
                transform.position = Bezier.GetBezier(startPos, middlePos1, middlePos2, endPos, time);
                transform.rotation = Quaternion.LookRotation(Bezier.BezierRotation(startPos, middlePos1, middlePos2, endPos, time));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x+90f, transform.eulerAngles.y, 0);
                while (PauseScript.IsPaused)
                    yield return null;
                time += Time.fixedDeltaTime*6/Vector3.Distance(startPos, endPos);
                yield return null;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Road"))
            {
                StopCoroutine(Move());
                Collider[] targets = Physics.OverlapSphere(transform.position, 3);
                
                Destroy(gameObject);
            }
        }
    }
}