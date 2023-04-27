using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopToggleScript : MonoBehaviour
{
    public GameObject Window;

    public static GameObject _window;

    public void OnClick()
    {
        if (_window != null)
            _window.gameObject.SetActive(false);
        _window = Window;
        Window.SetActive(true);
    }
}
