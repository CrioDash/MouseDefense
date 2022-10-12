using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    private Text _text;

    private float _currentFps;
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        _currentFps = 1 / Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _text.text = ((int)_currentFps).ToString();
    }
}
