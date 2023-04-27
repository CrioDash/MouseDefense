using System;
using GameData;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MenuSetData:MonoBehaviour
    {
        public static MenuSetData Instance;

        [Header("Settings", order = 0)]
        public UniversalRenderPipelineAsset URP;
        public Toggle shadowsToggle;
        public Slider renderScaleSlider;

        //[Header("Shop", order = 1)]
        
        
        
        
        private void Start()
        {
            LoadingScreen.Instance.StartCoroutine("CloseAnimation");
            Instance = this;
            renderScaleSlider.value = PlayerStats.Instance.RenderScale;
            shadowsToggle.isOn = PlayerStats.Instance.IsShadows;
        }
    }
}