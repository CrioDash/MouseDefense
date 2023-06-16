using System;
using UI;
using UI.Pause;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tiles
{
    public class TowerTile : MonoBehaviour
    {
        public enum TileType
        {
            Free, Towered
        }

        public TileType type;

        private Renderer _tileRenderer;
        private Color Color;

        private void Awake()
        {
            _tileRenderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            Color = _tileRenderer.sharedMaterial.color;
        }
        
        
        private void OnMouseOver()
        {
            if(PauseScript.IsPaused || EventSystem.current.IsPointerOverGameObject())
                return;
            if(type==TileType.Free && !TowerInfo.Info.IsOpened)
                _tileRenderer.material.color = Color.green;
        }

        private void OnMouseExit()
        {
            if(PauseScript.IsPaused )
                return;
            if(type==TileType.Free && !TowerInfo.Info.IsOpened)
                _tileRenderer.material.color = Color;
        }

        private void OnMouseUpAsButton()
        {
            if(PauseScript.IsPaused || EventSystem.current.IsPointerOverGameObject())
                return;
            if (TowerInfo.Info.IsOpened)
            {
                ConsumableWindow.Instance.Close();
                TowerInfo.Info.CloseWindow();
            }
            if (type == TileType.Free && !TowerInfo.Info.IsOpened)
            {
                _tileRenderer.material.color = Color;
                ConsumableWindow.Instance.Close();
                TowerInfo.Info.ShowBuildWindow(this);
            }
        }
    }
}
