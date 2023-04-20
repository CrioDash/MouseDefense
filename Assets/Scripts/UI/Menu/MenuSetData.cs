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

        public UniversalRenderPipelineAsset URP;
        public Toggle shadowsToggle;
        public Slider renderScaleSlider;
        
        private void Start()
        {
            Instance = this;
            URP.renderScale = 1;
            renderScaleSlider.value = PlayerStats.Instance.RenderScale;
            shadowsToggle.isOn = PlayerStats.Instance.IsShadows;
        }
    }
}