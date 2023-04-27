using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Enemies;
using GameData;
using UnityEngine;

namespace Consumables
{
    public class PoisonCloudScript:MonoBehaviour
    {
        private static Dictionary<int, List<Color>> _renderers;

        private static Dictionary<GameObject, bool> _enemies;

        private float cd = 1f;
        private float time;

        private void Start()
        {
            if (_renderers == null)
                _renderers = new Dictionary<int, List<Color>>();
            if (_enemies == null)
                _enemies = new Dictionary<GameObject, bool>();
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(7f);
            Renderer rend0 = transform.GetChild(0).GetComponent<Renderer>();
            Renderer rend1 = transform.GetChild(1).GetComponent<Renderer>();
            float t = 0;
            Color clr = rend0.material.color;
            while (t<1)
            {
                rend0.material.color = Color.Lerp(clr, Color.clear, t);
                rend1.material.color = rend0.material.color;
                t += Time.deltaTime * 2;
                yield return null;
            }
            Vector3 pos = transform.position;
            pos.y -= 200;
            transform.position = pos;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Enemy"))
                return;
            if (!_enemies.ContainsKey(other.gameObject))
            {
                Debug.Log(other.GetInstanceID());
                _enemies.Add(other.gameObject, true);
            }
            _enemies[other.gameObject] = true;
            if (!_renderers.ContainsKey(other.GetInstanceID()))
            {
                _renderers.Add(other.GetInstanceID(), new List<Color>());
                foreach (Renderer rend in other.GetComponentsInChildren<Renderer>())
                {
                    Color clr = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 1);
                    _renderers[other.GetInstanceID()].Add(clr);
                }
            }
            foreach (Renderer rend in other.GetComponentsInChildren<Renderer>())
            {
                StartCoroutine(PaintColor(true, rend, rend.material.color, new Color(0, 0.5f, 0.1f, 1),
                    other.gameObject));
            }
            

        }

        private void OnTriggerStay(Collider other)
        {
            time += Time.deltaTime;
            if(other.CompareTag("Enemy"))
                _enemies[other.gameObject] = true;
            if(time<cd)
                return;
            time = 0;
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage
                    .TakeDamage(PlayerStats.Instance.PoisonDamage, DamageType.Periodical);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.CompareTag("Enemy"))
                return;
            Renderer[] rend = other.GetComponentsInChildren<Renderer>();
            _enemies[other.gameObject] = false;
            for(int i = 0; i<rend.Length; i++)
            {
                StartCoroutine(PaintColor(false, rend[i], new Color(0, 0.50f, 0.100f,1),
                    _renderers[other.GetInstanceID()][i], other.gameObject));
            }
            
        }

        private IEnumerator PaintColor(bool state, Renderer rend, Color start, Color end, GameObject gm)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log(gm.name);
            if(_enemies[gm]!=state)
                yield break;
            float t = 0;
            Color clr = rend.material.color;
            while (t<1)
            {
                rend.material.color = Color.Lerp(start, end, t);
                t += Time.deltaTime * 2;
                yield return null;
            }
            rend.material.color = end;
        }
        
        
        
        
    }
}