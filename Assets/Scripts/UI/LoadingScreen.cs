using System;
using System.Collections;
using Events;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;

namespace UI
{
    public class LoadingScreen:MonoBehaviour
    {

        public static LoadingScreen Instance;
            
        public bool Fade = false;
        
        private Image _image;
        

        private void Awake()
        {
            _image = GetComponent<Image>();
            Instance = this;
        }

        private void Start()
        {
            _image.raycastTarget = true;
            _image.color = Fade ? Color.black : Color.clear;
        }


        public IEnumerator CloseAnimation()
        {
            _image.color = Color.black;
            yield return new WaitForSecondsRealtime(1f);
            float t = 0;
            while (t<1)
            {
                _image.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), t);
                t += Time.unscaledDeltaTime*4;
                yield return null;
            }
            _image.color = Color.clear;
            _image.raycastTarget = false;
            if(PauseScript.IsPaused)
                EventBus.Publish(EventType.PAUSE);
        }
        
        public IEnumerator ShowAnimation()
        {
            _image.color = Color.clear;
            float t = 0;
            while (t<1)
            {
                _image.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), t);
                t += Time.unscaledDeltaTime*4;
                yield return null;
            }
            _image.color = Color.black;
            if(PauseScript.IsPaused)
                EventBus.Publish(EventType.PAUSE);
        }
    }
}