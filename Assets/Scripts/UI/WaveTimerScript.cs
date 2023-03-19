using System;
using System.Collections;
using System.Timers;
using TMPro;
using UI.Pause;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class WaveTimerScript:MonoBehaviour
    {
        public bool waited = false;
        public static WaveTimerScript Instance;

        private CanvasGroup _group;
        private TextMeshProUGUI _text;
        private float t = 0;

        private void Start()
        {
            _group = GetComponent<CanvasGroup>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            Instance = this;
        }

        public void TimerStart(int time)
        {
            _group.alpha = 1;
            StartCoroutine(StartTiming(time));
        }

        public IEnumerator StartTiming(int time)
        {
            waited = false;
            t = 0;
            while (t<1)
            {
                _group.alpha = Mathf.Lerp(0, 1, t);
                t += Time.deltaTime*4;
                yield return null;
            }
            t = time;
            while (t>0 && !waited)
            {
                t -= Time.deltaTime;
                _text.text = Mathf.CeilToInt(t).ToString();
                yield return null;
            }
            t = 0;
            while (t<1)
            {
                _group.alpha = Mathf.Lerp(1, 0, t);
                t += Time.deltaTime*4;
                yield return null;
            }
            waited = true;
            _group.alpha = 0;
        }

        public void StopTiming()
        {
            Level.currentLevel.ChangeMoney(Mathf.CeilToInt(t)*5);
            waited = true;
        }
    }
}