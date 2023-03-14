using UnityEngine;

namespace UI.Pause
{
    public class BulletPause: MonoBehaviour, IPausable
    {
        
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
            throw new System.NotImplementedException();
        }
    }
}