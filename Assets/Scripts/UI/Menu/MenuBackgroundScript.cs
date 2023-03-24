using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackgroundScript : MonoBehaviour
{
    public GameObject Background { set; get; }
    
    private CanvasGroup _group;
    
    void Start()
    {
        Background = GetComponentInChildren<Image>().gameObject;
        Background.transform.localPosition = new Vector3(0, 0, transform.GetComponent<RectTransform>().rect.width/2+150);
        _group = GetComponent<CanvasGroup>();
    }
    

    public IEnumerator Fade(float alpha1 ,float alpha2)
    {
        float t = 0;
        while (t < 1)
        {
            Background.transform.rotation = Quaternion.Euler(Vector3.zero);
            _group.alpha = Mathf.Lerp(alpha1, alpha2, t);
            t += Time.deltaTime*2;
            yield return null;
        }

        _group.alpha = alpha2;
    }
}
