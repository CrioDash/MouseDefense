using System;
using System.Linq;
using Events;
using Game;
using Tiles;
using TMPro;
using Towers;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using EventType = Events.EventType;

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
            _cost = TowerInfo.Info.towerPrefabs.Find(x => x.GetComponent<Tower>().type == Type).GetComponent<Tower>().cost;
            GetComponentInChildren<TextMeshProUGUI>().text = _cost.ToString();
            if (!PlayerStats.Towers.Contains(Type))
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
            _button.interactable = Level.currentLevel.Gold >= _cost;
        }

        public void CreateTower()
        {
            GameObject tower = Instantiate(TowerInfo.Info.towerPrefabs[(int)Type]);
            Vector3 pos = TowerInfo.Info.TowerPos;
            pos.y += 1;
            tower.transform.position = pos;
            TowerInfo.Info.TowerTile.type = TowerTile.TileType.Towered;
            tower.GetComponent<Tower>().tile = TowerInfo.Info.TowerTile;
            Level.currentLevel.ChangeMoney(-_cost);
            TowerInfo.Info.CloseWindow();
        }

    
    }
}
