using System.Linq;
using Game;
using Tiles;
using Towers;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        public TowerType Type;
        public int cost;
        
        private Level _level;
        private TowerBuilder _builder;
        private Button _button;
    

        private void Awake()
        {
            _builder = GetComponentInParent<TowerBuilder>();
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            GetComponentInChildren<Text>().text = cost.ToString();
            
            if (!PlayerStats.Towers.Contains(Type))
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            _level = FindObjectOfType<Level>();
        }

        private void Update()
        {
            if(PauseScript.IsPaused)
                return;
            _button.interactable = _level.Gold >= cost;
        }

        public void CreateTower()
        {
            GameObject tower = Instantiate(_builder.towerPrefabs[(int)Type]);
            Vector3 pos = _builder.towerPos;
            pos.y += 1;
            tower.transform.position = pos;
            _builder.towerTile.type = TowerTile.TileType.Towered;
            _level.ChangeMoney(-cost);
            _builder.StartCoroutine(_builder.CloseAnimation());
        }

    
    }
}
