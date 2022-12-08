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

        [HideInInspector] public bool stopClosing = false;
        [HideInInspector] public bool opened = false;
        [HideInInspector] public Tower tower;
        [HideInInspector] public Vector3 towerPos;
        [HideInInspector] public TowerTile towerTile;

        public static TowerInfo Info;

        private void Awake()
        {
            foreach (Button gm in GetComponentsInChildren<Button>())
            {
                gm.gameObject.AddComponent<ButtonPause>();
            }

            Info = this;
        }

        private void FixedUpdate()
        {
            if (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended && opened)
                StartCoroutine(CloseAnimation());
        }

        public void CloseWindow()
        {
            StartCoroutine(CloseAnimation());
        }

        public void ShowSellWindow(Tower tower)
        {
            opened = true;
            buildContainer.SetActive(false);
            sellContainer.SetActive(true);
            this.tower = tower;
            towerPos = tower.transform.position;
            StartCoroutine(ShowAnimation());
        }
        
        public void ShowBuildWindow(TowerTile tile)
        {
            opened = true;
            sellContainer.SetActive(false);
            buildContainer.SetActive(true);
            towerPos = tile.transform.position;
            towerTile = tile;
            StartCoroutine(ShowAnimation());
        }

        public void SellTower()
        {
            Level.currentLevel.ChangeMoney((tower.cost+tower.cost*1/2*(int)Math.Truncate(Convert.ToDecimal(tower.level / 2))
                                                      +tower.cost*3/4*(int)Math.Truncate(Convert.ToDecimal(tower.level / 3)))/2);
            tower.tile.type = TowerTile.TileType.Free;
            Destroy(tower.gameObject);
            StartCoroutine(CloseAnimation());
        }

        public IEnumerator ShowAnimation()
        {
            StartCoroutine(StopClosing());
            transform.position = towerPos;
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

        public IEnumerator CloseAnimation()
        {
            float t = 0;
            while (t<1)
            {
                if(stopClosing)
                    yield break;
                while (PauseScript.IsPaused)
                {
                    yield return null;
                }
                transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.25f, 0.25f), t);
                t += Time.fixedDeltaTime*8;
                yield return null;
            }
            Vector3 pos = transform.position;
            pos.y = 100;
            transform.position = pos;
            opened = false;
        }
        
        public IEnumerator StopClosing()
        {
            stopClosing = true;
            yield return new WaitForSeconds(0.05f);
            stopClosing = false;
        }

    }
}