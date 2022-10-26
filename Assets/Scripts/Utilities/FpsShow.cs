using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class FpsShow : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private float _currentFps;
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {
        
        }
    
        void Update()
        {
            _currentFps = 1 / Time.deltaTime;
        }

        private void FixedUpdate()
        {
            _text.text = ((int)_currentFps).ToString();
        }
    }
}
