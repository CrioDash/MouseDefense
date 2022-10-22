using System.Collections;
using Events;
using Game;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;

namespace UI
{
    public class MoneyShow : MonoBehaviour
    {
        public Text Text;

        private int _gold;
    
        private void OnEnable()
        {
            EventBus.Subscribe(EventType.MONEYCHANGE, ChangeMoneyText);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.MONEYCHANGE, ChangeMoneyText);
        }

        private void Start()
        {
            _gold = Variables.Gold;
            Text.text = _gold.ToString();
        }

        public void ChangeMoneyText()
        {
            StartCoroutine(MoneyCoroutine());
        }

        private IEnumerator MoneyCoroutine()
        {
            yield return null;
            Level curLevel = FindObjectOfType<Level>();
            float time = 0;
            while (time<1)
            {
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                _gold = Mathf.RoundToInt(Mathf.Lerp(_gold, curLevel.Gold, time));
                Text.text = _gold.ToString();
                time += Time.fixedDeltaTime*8;
                yield return null;
            }
            if (_gold != curLevel.Gold)
            {
                _gold = (int)curLevel.Gold;
                Text.text = _gold.ToString();
            }
        }
    }
}
