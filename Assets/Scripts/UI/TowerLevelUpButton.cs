using System;
using TMPro;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

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
            TowerInfo.Info.StartCoroutine("StopClosing");
            _info.tower.LevelUp();
            _info.tower.level++;
            if (_info.tower.level == 3)
                _button.interactable = false;
            Level.currentLevel.ChangeMoney(-cost);
            cost = (int)Math.Truncate(_info.tower.cost * (((float)_info.tower.level+1) / 4));
            GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
            
        }

        private void Update()
        {
            if(PauseScript.IsPaused)
                return;
            if (_info.tower != null)
            {
                cost = (int)Math.Truncate(_info.tower.cost * (((float)_info.tower.level+1) / 4));
                GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
                if (_info.tower.level==3)
                    return;
                _button.interactable = Level.currentLevel.Gold >= cost;
            }
            
        }
    }
}