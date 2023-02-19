using System;
using System.Collections;
using System.Collections.Generic;
using Tiles;
using UI.Pause;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utilities;
using TouchPhase = UnityEngine.TouchPhase;

namespace UI
{
    public class TowerInfo : MonoBehaviour
    {
        public List<GameObject> towerPrefabs;
        public GameObject buildContainer;
        public GameObject sellContainer;
        
        public bool IsOpened { set; get; }
        public Tower InfoTower { set; get; }
        public Vector3 TowerPos { set; get; }
        public TowerTile TowerTile { set; get; }

        public static TowerInfo Info;

        private void Awake()
        {
            foreach (Button gm in GetComponentsInChildren<Button>())
            {
                gm.gameObject.AddComponent<ButtonPause>();
            }

            Info = this;
        }
        

        public void CloseWindow()
        {
            StartCoroutine(CloseAnimation());
        }

        public void ShowSellWindow(Tower tower)
        {
            IsOpened = true;
            buildContainer.SetActive(false);
            sellContainer.SetActive(true);
            InfoTower = tower;
            TowerPos = tower.transform.position;
            StartCoroutine(ShowAnimation());
        }
        
        public void ShowBuildWindow(TowerTile tile)
        {
            IsOpened = true;
            sellContainer.SetActive(false);
            buildContainer.SetActive(true);
            TowerPos = tile.transform.position;
            TowerTile = tile;
            StartCoroutine(ShowAnimation());
        }

        public void SellTower()
        {
            Level.currentLevel.ChangeMoney((InfoTower.cost+InfoTower.cost*1/2*(int)Math.Truncate(Convert.ToDecimal(InfoTower.Level / 2))
                                                      +InfoTower.cost*3/4*(int)Math.Truncate(Convert.ToDecimal(InfoTower.Level / 3)))/2);
            InfoTower.tile.type = TowerTile.TileType.Free;
            Destroy(InfoTower.gameObject);
            CloseWindow();
        }

        private IEnumerator ShowAnimation()
        {
            transform.position = TowerPos;
            Vector3 pos;
            pos = transform.localPosition;
            pos.z = 0;
            transform.localPosition = pos;
            float t = 0;
            while (t<1)
            {
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                transform.localScale = Vector3.Lerp(new Vector3(0.25f, 0.25f),Vector3.one , t);
                t += Time.fixedDeltaTime*8;
                yield return null;
            }
            
        }

        private IEnumerator CloseAnimation()
        {
            if(InfoTower!=null)
                InfoTower.radiusSprite.color = new Color(1, 1, 1, 0);
            float t = 0;
            while (t<1)
            {
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.25f, 0.25f), t);
                t += Time.fixedDeltaTime*8;
                yield return null;
            }
            Vector3 pos = transform.position;
            pos.y = 200;
            transform.position = pos;
            IsOpened = false;
        }
        

    }
}