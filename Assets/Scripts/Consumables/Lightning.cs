using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Consumables
{
    public class Lightning:MonoBehaviour
    {
        private LineRenderer _line;
        private void Start()
        {
            _line = GetComponent<LineRenderer>();
            StartCoroutine(LaunchZipZap());
        }

        private IEnumerator LaunchZipZap()
        {
            WaitForSeconds wait0025 = new WaitForSeconds(0.0125f);
            WaitForSeconds wait1 = new WaitForSeconds(1);
            while (true)
            {
                _line.SetPosition(0,transform.position);
                while (_line.GetPosition(_line.positionCount - 1).y > -5)
                {
                    _line.positionCount++;
                    Vector3 point = Random.onUnitSphere;
                    point = new Vector3(point.x, -Mathf.Abs(point.y)*4, point.z);
                    _line.SetPosition(_line.positionCount - 1, _line.GetPosition(_line.positionCount - 2) + point);
                    yield return wait0025;
                }
                _line.positionCount = 1;
                
                yield return wait1;
            }
        }
    }
}