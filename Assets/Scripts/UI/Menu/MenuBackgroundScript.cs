using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackgroundScript : MonoBehaviour
{
    private GameObject _background;
    private CanvasGroup _group;
    
    void Start()
    {
        _background = GetComponentInChildren<Image>().gameObject;
        _background.transform.localPosition = new Vector3(0, 0, transform.GetComponent<RectTransform>().rect.width/2+150);
        _group = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        _background.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
    }

    public IEnumerator Fade(float alpha1 ,float alpha2)
    {
        float t = 0;
        while (t < 1)
        {
            _group.alpha = Mathf.Lerp(alpha1, alpha2, t);
            t += Time.fixedDeltaTime*2;
            yield return null;
        }

        _group.alpha = alpha2;
    }
}
