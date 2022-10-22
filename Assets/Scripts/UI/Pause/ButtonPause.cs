using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pause
{
    public class ButtonPause: MonoBehaviour, IPausable
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            PauseScript._pauses.Add(this);
        }
        
        private void OnDisable()
        {
            PauseScript._pauses.Remove(this);
        }

        public void Pause()
        {
            _button.interactable = !PauseScript.IsPaused;
        }
    }
}