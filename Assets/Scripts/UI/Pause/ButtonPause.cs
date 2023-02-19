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
            if (_button == null)
                _button = GetComponentInChildren<Button>();
        }

        private void OnEnable()
        {
            PauseScript.Pauses.Add(this);
        }
        
        private void OnDisable()
        {
            PauseScript.Pauses.Remove(this);
        }

        public void Pause()
        {
            if(!PauseScript.ExceptionPauses.Contains(this))
                _button.interactable = !PauseScript.IsPaused;
        }
    }
}