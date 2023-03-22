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
            while (true)
            {
                _line.SetPosition(0,transform.position);
                while (_line.GetPosition(_line.positionCount - 1).y > -5)
                {
                    _line.positionCount++;
                    Vector3 point = Random.onUnitSphere;
                    point = new Vector3(point.x, -Mathf.Abs(point.y)*4, point.z);
                    _line.SetPosition(_line.positionCount - 1, _line.GetPosition(_line.positionCount - 2) + point);
                    yield return null;
                }
                _line.positionCount = 1;
                
                yield return new WaitForSeconds(1f);
            }
        }
    }
}