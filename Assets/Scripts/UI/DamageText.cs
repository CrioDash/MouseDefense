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

        private void FixedUpdate()
        {
            Vector3 pos = transform.position;
            pos.x += 10 * Time.fixedDeltaTime;
            pos.y += 20 * Time.fixedDeltaTime;
            transform.position = pos;
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - 0.04f);
        }
    }
}