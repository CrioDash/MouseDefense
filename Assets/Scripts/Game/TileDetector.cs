using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class TileDetector : MonoBehaviour
{
    public TowerBuilder builder;

    private Material _towerPlaceMat;
    private GameObject _selectedTile;
    private RaycastHit _raycastHit;
    private Renderer _selectedTileRenderer;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Start()
    {
        StartCoroutine(BuildTile());
    }
    
    public IEnumerator BuildTile()
    {
        while (true)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            yield return new WaitUntil(() => Input.touchCount != 0);
            PaintOldTile();
            LaunchRaycast();
            PaintNewTile();
        }
    }

    void PaintOldTile()
    {
        if (_selectedTile != null)
            _selectedTileRenderer.material.color =
                _towerPlaceMat.color;
    }

    void LaunchRaycast()
    {
        if(Input.touchCount==0)
            return;
        Vector2 touchPos = Input.GetTouch(0).position;
        Ray screenRay = _camera.ScreenPointToRay(touchPos);
        Physics.Raycast(screenRay, out _raycastHit);
    }

    void PaintNewTile()
    {
        if(CheckStuff())
            return;
        _selectedTile = _raycastHit.collider.gameObject;
        _selectedTileRenderer = _selectedTile.GetComponent<Renderer>();
        if (_towerPlaceMat == null)
            _towerPlaceMat = _selectedTileRenderer.sharedMaterial;
        _selectedTileRenderer.material.color = Color.green;
        Touch currentTouch = Input.GetTouch(0);
        if (currentTouch.phase == TouchPhase.Ended)
        {
            builder.Show(_raycastHit.transform.position, _raycastHit.collider.GetComponent<TowerTile>());
        }
    }

    public bool CheckStuff()
    {
        if(_raycastHit.collider==null)
            return true;
        if(!_raycastHit.collider.gameObject.CompareTag("TowerPlaceTile"))
            return true;
        TowerTile tile = _raycastHit.collider.GetComponent<TowerTile>();
        if(tile.Type == TowerTile.TileType.Towered)
            return true;
        if(builder.gameObject.activeSelf) 
            return true;
        return false;
    }
    
}
