using System;
using Events;
using TMPro;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using EventType = Events.EventType;

namespace UI
{
    public class TowerLevelUpButton:MonoBehaviour
    {
        private Button _button;
        private TowerInfo _info;
        private int cost;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _info = GetComponentInParent<TowerInfo>();
        }
        
        public void LevelUp()
        {
            _info.InfoTower.LevelUp();
            _info.InfoTower.Level++;
            if (_info.InfoTower.Level == 3)
                _button.interactable = false;
            Level.Instance.ChangeMoney(-cost);
            cost = (int)Math.Truncate(_info.InfoTower.cost * (((float)_info.InfoTower.Level+1) / 4));
            GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
            UpdateButton();
        }

        public void OnEnable()
        {
            EventBus.Subscribe(EventType.MONEYCHANGE, UpdateButton);
        }

        public void OnDisable()
        {
            EventBus.Unsubscribe(EventType.MONEYCHANGE, UpdateButton);
        }

        public void UpdateButton()
        {
            if (_info.InfoTower!=null)
            {
                cost = (int)Math.Truncate(_info.InfoTower.cost * (((float)_info.InfoTower.Level+1) / 4));
                GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
                if (_info.InfoTower.Level==3)
                    return;
                _button.interactable = Level.Instance.Gold >= cost;
            }
            
        }
    }
}