using System.Linq;
using Towers;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerBuildButton : MonoBehaviour
    {
        public TowerType Type;
    
        private int _cost;
        private Level _level;
        private TowerBuilder _builder;
        private Button _button;
    

        private void Awake()
        {
            if (!PlayerStats.Towers.Contains(Type))
            {
                gameObject.SetActive(false);
            }

            _button = GetComponent<Button>();
        }

        private void Start()
        {
            GetComponentInChildren<Text>().text = _cost.ToString();
            _builder = GetComponentInParent<TowerBuilder>();
        }

        private void OnEnable()
        {
            _level = FindObjectOfType<Level>();
        }

        private void Update()
        {
            if(PauseScript.IsPaused)
                return;
            _button.interactable = _level.Gold > _cost;
        }

        public void CreateTower()
        {
            GameObject tower = Instantiate(_builder.Towers[Type]);
            Vector3 pos = _builder.towerPos;
            pos.y += 1;
            tower.transform.position = pos;
            _builder.towerTile.Type = TowerTile.TileType.Towered;
            _level.ChangeMoney(-_cost);
            _builder.StartCoroutine(_builder.CloseAnimation());
        }

    
    }
}
