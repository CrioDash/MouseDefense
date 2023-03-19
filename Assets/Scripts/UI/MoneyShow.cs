using System.Collections;
using Events;
using Game;
using TMPro;
using UI.Pause;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;
using EventType = Events.EventType;

namespace UI
{
    public class MoneyShow : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public GameObject TextPrefab;

        private int _gold = 0;
    
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
            Text.text = _gold.ToString();
        }

        public void ChangeMoneyText()
        {
            StartCoroutine(MoneyCoroutine());
            StartCoroutine(FlowText());
        }

        private IEnumerator MoneyCoroutine()
        {
            yield return null;
            float time = 0;
            while (time<1)
            {
                _gold = Mathf.RoundToInt(Mathf.Lerp(_gold, Level.currentLevel.Gold, time));
                Text.text = _gold.ToString();
                time += Time.deltaTime*4;
                yield return null;
            }
            if (_gold != Level.currentLevel.Gold)
            {
                _gold = (int)Level.currentLevel.Gold;
                Text.text = _gold.ToString();
            }
            
        }

        private IEnumerator FlowText()
        {
            TextMeshProUGUI prefab = Instantiate(TextPrefab, transform).GetComponent<TextMeshProUGUI>();
            prefab.text = Mathf.Abs(_gold - Level.currentLevel.Gold).ToString();
            if(_gold<Level.currentLevel.Gold)
            {
                prefab.text = "+" + prefab.text;
                prefab.color = Color.green;
            }
            else if (_gold>Level.currentLevel.Gold)
            {
                prefab.text = "-" + prefab.text;
                prefab.color = Color.red;
            }
            else
            {
                Destroy(prefab.gameObject);
                yield break;
            }
            Color color = prefab.color;
            color.a = 0;
            float time = 0;
            while (time<1)
            {
                prefab.color = Color.Lerp(prefab.color, color, time);
                Text.text = _gold.ToString();
                time += Time.deltaTime;
                yield return null;
            }

            Destroy(prefab.gameObject);
        }
    }
}
