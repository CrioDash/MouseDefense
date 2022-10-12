using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Towers;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    public List<GameObject> TowerTypes;
    public Dictionary<TowerType, GameObject> Towers = new Dictionary<TowerType, GameObject>();
    public Vector3 TowerPos;
    public TowerTile TowerTile;
    
    private List<TowerBuildButton> TowerButtons;


    private void Awake()
    {
        foreach (Button gm in GetComponentsInChildren<Button>())
        {
            gm.gameObject.AddComponent<ButtonPause>();
        }
    }

    private void Start()
    {
        foreach (GameObject gm in TowerTypes)
        {
            Towers.Add(gm.GetComponent<Tower>().Type, gm);
        }
        TowerButtons = gameObject.GetComponentsInChildren<TowerBuildButton>().ToList();
    }

    public void Show(Vector3 pos, TowerTile tile)
    {
        TowerPos = pos;
        TowerTile = tile;
        gameObject.SetActive(true);
        transform.position = pos;
        pos = transform.localPosition;
        pos.z = 0;
        transform.localPosition = pos;
        StartCoroutine(ShowAnimation());
    }

    public void Close()
    {
        StartCoroutine(CloseAnimation());
    }

    
    
    public IEnumerator ShowAnimation()
    {
        float t = 0;
        while (t<1)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f),Vector3.one , t);
            t += Time.fixedDeltaTime*8;
            yield return null;
        }
    }

    public IEnumerator CloseAnimation()
    {
        float t = 0;
        while (t<1)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.5f, 0.5f), t);
            t += Time.fixedDeltaTime*8;
            yield return null;
        }

        Vector3 pos = transform.position;
        pos.y = 100;
        transform.position = pos;
        gameObject.SetActive(false);
    }
}