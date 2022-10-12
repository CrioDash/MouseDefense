using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Towers;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildButton : MonoBehaviour
{
    public TowerType Type;
    public int Cost;

    private Level _level;
    private TowerBuilder _builder;
    private Button _button;
    

    private void Awake()
    {
        if(!PlayerStats.Towers.Contains(Type))
            gameObject.SetActive(false);
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        GetComponentInChildren<Text>().text = Cost.ToString();
        _builder = GetComponentInParent<TowerBuilder>();
    }

    private void OnEnable()
    {
        _level = FindObjectOfType<Level>();
        StartCoroutine(CheckMoney());
    }

    private void OnDisable()
    {
        StopCoroutine(CheckMoney());
    }

    public void CreateTower()
    {
        GameObject tower = Instantiate(_builder.Towers[Type]);
        Vector3 pos = _builder.TowerPos;
        pos.y += 1;
        tower.transform.position = pos;
        _builder.TowerTile.Type = TowerTile.TileType.Towered;
        _level.ChangeMoney(-Cost);
        _builder.StartCoroutine(_builder.CloseAnimation());
    }

    private IEnumerator CheckMoney()
    {
        while (true)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            _button.interactable = !(_level.Gold < Cost);
            yield return null;
        }
        
    }
}
