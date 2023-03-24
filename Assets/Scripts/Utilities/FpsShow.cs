using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class FpsShow : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private float _currentFps;
        private float _delay = 0.25f;
        private float _timer = 0.0f;
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {
        
        }
    
        void Update()
        {
            _timer += Time.deltaTime;
            if(_timer<_delay)
                return;
            _currentFps = 1 / Time.smoothDeltaTime;
            _timer -= _delay;
        }

        private void FixedUpdate()
        {
            _text.text = ((int)_currentFps).ToString();
        }
    }
}
