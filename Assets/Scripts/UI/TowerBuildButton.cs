using System;
using System.Linq;
using Events;
using Game;
using GameData;
using Tiles;
using TMPro;
using Towers;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using EventType = Events.EventType;
using TowerType = GameData.Variables.TowerType;

namespace UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        public TowerType Type;

        private int _cost;
        private Button _button;
    

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        

        private void Start()
        {
            _cost = TowerInfo.Info.Towers[Type].cost;
            GetComponentInChildren<TextMeshProUGUI>().text = _cost.ToString();
            if (!PlayerStats.Instance.Towers.ContainsKey(Type))
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            EventBus.Subscribe(EventType.MONEYCHANGE, UpdateButton);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.MONEYCHANGE, UpdateButton);
        }

        public void UpdateButton()
        {
            _button.interactable = Level.Instance.Gold >= _cost;
        }

        public void CreateTower()
        {
            GameObject tower = Instantiate(TowerInfo.Info.Towers[Type].gameObject);
            Vector3 pos = TowerInfo.Info.TowerPos;
            pos.y += 1;
            tower.transform.position = pos;
            TowerInfo.Info.TowerTile.type = TowerTile.TileType.Towered;
            tower.GetComponent<Tower>().tile = TowerInfo.Info.TowerTile;
            Level.Instance.ChangeMoney(-_cost);
            TowerInfo.Info.CloseWindow();
        }

    
    }
}
