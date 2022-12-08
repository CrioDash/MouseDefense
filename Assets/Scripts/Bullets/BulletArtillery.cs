using System;
using System.Collections;
using Enemies;
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
        private float time;
        private Vector3 middlePos1;
        private Vector3 middlePos2;




        private void Start()
        {
            startPos = transform.position;
            endPos = Parent.GetTarget().transform.position;
            endPos.y = -0.5f;
            middlePos1 = Vector3.Lerp(startPos, endPos, 1 / 3f);
            middlePos2 = Vector3.Lerp(startPos, endPos, 2 / 3f);
            middlePos1.y += 6;
            middlePos2.y += 6;
        }

        public void FixedUpdate()
        {
            if (PauseScript.IsPaused)
            {
                return;
            }

            if (time < 1)
            {
                transform.position = Bezier.GetBezier(startPos, middlePos1, middlePos2, endPos, time);
                transform.rotation =
                    Quaternion.LookRotation(Bezier.BezierRotation(startPos, middlePos1, middlePos2, endPos, time));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90f, transform.eulerAngles.y, 0);
                time += Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Road") || other.CompareTag("Enemy"))

    {
                Collider[] targets = Physics.OverlapBox(transform.position, new Vector3(2.5f,2.5f,2.5f), Quaternion.identity, 1<<7);
                foreach (Collider col in targets)
                {
                    col.GetComponent<Enemy>().TakeDamage.TakeDamage(GetDmg(), DamageType.Splash);
                }
                Destroy(gameObject);
            }
        }
        
    }
}