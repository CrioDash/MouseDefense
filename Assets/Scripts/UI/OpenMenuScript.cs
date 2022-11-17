using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;


public class OpenMenuScript : MonoBehaviour
{
    public GameObject MenuButton;
    public GameObject MenuWindow;
    public GameObject startPoint;
    public GameObject endPoint;

    private bool _opened = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ButtonClick();

    }

    public void ButtonClick()
    {
        if(!_opened)
            OpenMenu();
        else
            CloseMenu();
    }

    public void OpenMenu()
    {
        EventBus.Publish(EventType.PAUSE);
        _opened = true;
        StopCoroutine(CloseAnimation());
        StartCoroutine(OpenAnimation());
    }

    public void CloseMenu()
    {
        EventBus.Publish(EventType.PAUSE);
        _opened = false;
        StopCoroutine(OpenAnimation());
        StartCoroutine(CloseAnimation());
    }

    public IEnumerator CloseAnimation()
    {
        float t = 0;
        
        while (t<1f)
        {
            MenuButton.transform.localEulerAngles = Vector3.Lerp(new Vector3(0, 0, -180),Vector3.zero,  t * 2);
            MenuWindow.transform.position = Vector3.Lerp(endPoint.transform.position,startPoint.transform.position, t);
            t += Time.fixedDeltaTime*4;
            yield return null;
        }
    }

    public IEnumerator OpenAnimation()
    {
        float t = 0;
        while (t<1f)
        {
            MenuButton.transform.localEulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, -180), t * 2);
            MenuWindow.transform.position = Vector3.Lerp(startPoint.transform.position, endPoint.transform.position, t);
            t += Time.fixedDeltaTime*4;
            yield return null;
        }
    }
    
}
