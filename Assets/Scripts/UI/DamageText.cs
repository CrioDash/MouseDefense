using System;
using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DamageText: MonoBehaviour
    {
        private TextMeshProUGUI _text;

        void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            StartCoroutine(ToPool());
        }

        public IEnumerator ToPool()
        {
            yield return new WaitForSeconds(1f);
            Level.Instance.TextPool.Add(_text);
        }

        private void Update()
        {
            Vector3 pos = transform.position;
            pos.x += 10 * Time.deltaTime;
            pos.y += 20 * Time.deltaTime;
            transform.position = pos;
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - 0.04f);
        }
    }
}