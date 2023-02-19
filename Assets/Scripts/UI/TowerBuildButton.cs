using System.Linq;
using Game;
using Tiles;
using TMPro;
using Towers;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

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

        private void FixedUpdate()
        {
            if (PauseScript.IsPaused)
                return;
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
            TowerInfo.Info.StartCoroutine("CloseAnimation");
        }

    
    }
}
