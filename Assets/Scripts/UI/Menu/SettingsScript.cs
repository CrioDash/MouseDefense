using GameData;
using UnityEngine;

namespace UI.Menu
{
    public class SettingsScript:MonoBehaviour
    {
        public void ShadowsToggle()
        {
            PlayerStats.Instance.IsShadows = MenuSetData.Instance.shadowsToggle.isOn;
        }

        public void RenderScaleSlider()
        {
            PlayerStats.Instance.RenderScale = MenuSetData.Instance.renderScaleSlider.value;
        }
    }
}