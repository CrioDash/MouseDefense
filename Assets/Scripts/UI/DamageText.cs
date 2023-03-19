using System;
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

        private void Start()
        {
           Destroy(gameObject, 0.5f);
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