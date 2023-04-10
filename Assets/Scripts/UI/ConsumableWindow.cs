using System;
using System.Collections;
using Events;
using UI.Pause;
using UnityEngine;
using EventType = Events.EventType;

namespace UI
{
    public class ConsumableWindow:MonoBehaviour
    {
        public GameObject startPos;
        public GameObject endPos;

        public bool Opened {set;get; 
    }

        public static ConsumableWindow Instance;

        private void OnEnable()
        {
            EventBus.Subscribe(EventType.PAUSE, Close);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.PAUSE, Close);
        }

        public void Start()
        {
            Instance = this;
        }

        public void ButtonClick()
        {
            if(!Opened)
                Open();
            else
                Close();
        }

        public void Open()
        {
            TowerInfo.Info.CloseWindow();
            Opened = true;
            StopCoroutine(CloseAnimation());
            StartCoroutine(OpenAnimation());
        }

        public void Close()
        {
            if(!Opened)
                return;
            Opened = false;
            StopCoroutine(OpenAnimation());
            StartCoroutine(CloseAnimation());
        }
        
        private IEnumerator OpenAnimation()
        {
            float t = 0;
        
            while (t<1f)
            {
                transform.position = Vector3.Lerp(startPos.transform.position,endPos.transform.position, t);
                t += Time.unscaledDeltaTime*8;
                yield return null;
            }

            transform.position = endPos.transform.position;
        }

        private IEnumerator CloseAnimation()
        {
            float t = 0;
        
            while (t<1f)
            {
                transform.position = Vector3.Lerp(endPos.transform.position,startPos.transform.position, t);
                t += Time.unscaledDeltaTime*8;
                yield return null;
            }

            transform.position = startPos.transform.position;
        }
    }
}