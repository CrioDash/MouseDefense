using System;
using System.Collections;
using Enemies;
using GameData;
using UI.Pause;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using Utilities;

namespace Bullets
{
    public class BulletArtillery : Bullet
    {
        private Vector3 startPos;
        private Vector3 endPos;
        private Vector3 middlePos1;
        private Vector3 middlePos2;

        public override IEnumerator Move()
        {
            startPos = transform.position;
            endPos = Parent.GetTarget().transform.position;
            endPos.y = -0.5f;
            middlePos1 = Vector3.Lerp(startPos, endPos, 1 / 3f);
            middlePos2 = Vector3.Lerp(startPos, endPos, 2 / 3f);
            middlePos1.y += 6;
            middlePos2.y += 6;

            float time = 0;
            while (time < 1)
            {
                transform.position = Bezier.GetBezier(startPos, middlePos1, middlePos2, endPos, time);
                transform.rotation =
                    Quaternion.LookRotation(Bezier.BezierRotation(startPos, middlePos1, middlePos2, endPos, time));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90f, transform.eulerAngles.y, 0);
                time += Time.deltaTime;
                yield return null;
            }
            Collider[] targets = Physics.OverlapSphere(transform.position, 10, 1<<2);
            foreach (Collider col in targets)
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if(enemy != null)
                    enemy.TakeDamage.TakeDamage(GetDmg(), DamageType.Splash);
            }
            ReturnToPool();
        }

    }
}