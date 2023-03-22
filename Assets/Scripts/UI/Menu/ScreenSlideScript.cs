using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenSlideScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public List<MenuBackgroundScript> MenuScripts = new List<MenuBackgroundScript>();
    public List<RectTransform> MenuSlides = new List<RectTransform>();
    public int sceneCount;

    private RectTransform _canvas;
    private int _activeScene = 1;
    private Vector2 _startPos;
    private int _moveDifference;
    private float _timeDifference;
    private Toggle[] _toggles;
   
    void Start()
    {
        _canvas = transform.root.GetComponent<RectTransform>();
        _toggles = FindObjectOfType<ToggleGroup>().GetComponentsInChildren<Toggle>();
        MenuSlides[0].sizeDelta = new Vector2(_canvas.rect.width, _canvas.rect.height);
        MenuSlides[0].transform.localPosition = new Vector3(-_canvas.rect.width / 2 + 3 , 0, 0);
        MenuSlides[1].sizeDelta = new Vector2(_canvas.rect.width, _canvas.rect.height);
        MenuSlides[1].transform.localPosition = new Vector3(MenuSlides[1].transform.localPosition.x,
            MenuSlides[1].transform.localPosition.y, -_canvas.rect.width / 2 );
        MenuSlides[2].sizeDelta = new Vector2(_canvas.rect.width, _canvas.rect.height);
        MenuSlides[2].transform.localPosition = new Vector3(_canvas.rect.width / 2 - 3, 0, 0);
        MenuSlides[_activeScene].SetAsLastSibling();
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        _moveDifference = (int)(_startPos.x - eventData.position.x);
        MenuSlides[_activeScene].SetAsLastSibling();
        if(_moveDifference<0 && _activeScene-1!=-1)
            MenuSlides[_activeScene-1].SetAsLastSibling();
        if(_moveDifference>0 && _activeScene+1!=MenuSlides.Count)
            MenuSlides[_activeScene+1].SetAsLastSibling();
        float rotationIndex = Mathf.Clamp(_moveDifference/(_canvas.rect.width/5), -1, 1);
        Debug.Log(_activeScene);
        if (_activeScene - 1 == -1)
            rotationIndex = Mathf.Clamp(rotationIndex, 0, 1);
        if (_activeScene+1==MenuSlides.Count)
            rotationIndex = Mathf.Clamp(rotationIndex, -1, 0);
        transform.localRotation = Quaternion.Euler(new Vector3(0,(_activeScene-1)*90f+30*rotationIndex,0));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _moveDifference = 0;
        _startPos = eventData.position;
        _timeDifference = Time.time;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        _timeDifference = Time.time - _timeDifference;
        int difference =  _moveDifference < 0 ? -1 : 1;
       if((Mathf.Abs(_moveDifference)>_canvas.rect.width/12 && _timeDifference<0.5f || Mathf.Abs(_moveDifference)>_canvas.rect.width/5) && _activeScene+difference!=MenuSlides.Count && _activeScene+difference != -1)
       {
           StartCoroutine(MenuScripts[_activeScene].Fade(1,0));
           _activeScene += difference;
           StartCoroutine(MenuScripts[_activeScene].Fade(0,1));
           _toggles[_activeScene].isOn = true;
       }
       MenuSlides[_activeScene].SetAsLastSibling();
       StartCoroutine(RotateAnimation(transform.localRotation.eulerAngles.y, (_activeScene - 1) * 90f));
       _moveDifference = 0;
       _timeDifference = Time.time;
    }

    IEnumerator RotateAnimation(float startAngle, float endAngle)
    {
        float t = 0;
        while (t<1)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0,startAngle,0),Quaternion.Euler(0,endAngle,0), t);
            t += Time.deltaTime*2;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0,endAngle,0);
    }
}
