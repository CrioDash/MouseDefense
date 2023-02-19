using System;
using UI;
using UI.Pause;
using UnityEngine;

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
        public Color Color;

        private void Awake()
        {
            _tileRenderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            Color = _tileRenderer.sharedMaterial.color;
        }


        private void OnMouseEnter()
        {
            if(PauseScript.IsPaused)
                return;
            if(type==TileType.Free && !TowerInfo.Info.IsOpened)
                _tileRenderer.material.color = Color.green;
        }

        private void OnMouseExit()
        {
            if(PauseScript.IsPaused)
                return;
            if(type==TileType.Free && !TowerInfo.Info.IsOpened)
                _tileRenderer.material.color = Color;
        }

        private void OnMouseUpAsButton()
        {
            if(PauseScript.IsPaused)
                return;
            if (TowerInfo.Info.IsOpened)
                TowerInfo.Info.CloseWindow();
            if (type == TileType.Free && !TowerInfo.Info.IsOpened)
            {
                _tileRenderer.material.color = Color;
                TowerInfo.Info.ShowBuildWindow(this);
            }
        }
    }
}
