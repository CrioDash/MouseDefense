using System.Collections.Generic;
using Enemies;
using GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DamageTextPool:MonoBehaviour
    {
        public GameObject TextPrefab;
        
        private Stack<TextMeshProUGUI> _texts = new Stack<TextMeshProUGUI>();
        
        
        public TextMeshProUGUI Get()
        {
            if (_texts.Count != 0)
            {
                TextMeshProUGUI text = _texts.Pop();
                text.gameObject.SetActive(true);
                return text;
            }
            else
            {
                return Instantiate(TextPrefab, Level.Instance.textContainer.transform).GetComponent<TextMeshProUGUI>();
            }
        }

        public void Add(TextMeshProUGUI text)
        {
            _texts.Push(text);
            text.gameObject.SetActive(false);
        }
    }
}