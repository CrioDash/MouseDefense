using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Consumables
{
    public class LightningStrike:MonoBehaviour
    {
        private LineRenderer _line;
        private Renderer _renderer;
        private void Start()
        {
            _line = GetComponentInChildren<LineRenderer>();
            _renderer = _line.GetComponent<Renderer>();
            StartCoroutine(LaunchZipZap());
        }

        private IEnumerator LaunchZipZap()
        {
            WaitForSeconds wait1 = new WaitForSeconds(0.5f);
            while (true)
            {
                transform.position = new Vector3(Random.Range(-20f, 20f), 60, Random.Range(-20f, 20f));

                _line.SetPosition(0,transform.position);
                while (_line.GetPosition(_line.positionCount - 1).y > 0)
                {
                    _line.positionCount++;
                    Vector3 point = Random.onUnitSphere;
                    point = new Vector3(point.x, -Mathf.Abs(point.y)*6, point.z);
                    _line.SetPosition(_line.positionCount - 1, _line.GetPosition(_line.positionCount - 2) + point);
                }
                _renderer.material.color = Color.white;
                yield return wait1;
                float t = 0;
                while (t < 1)
                {
                    _renderer.material.color = Color.Lerp(Color.white, new Color(1,1,1,0), t);
                    t += Time.deltaTime*2;
                    yield return null;
                }
                _renderer.material.color = Color.clear;
                _line.positionCount = 1;
            }
        }
    }
}