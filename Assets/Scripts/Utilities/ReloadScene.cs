using UI.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    [RequireComponent(typeof(ButtonPause))]
    public class ReloadScene : MonoBehaviour
    {
        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
