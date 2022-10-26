using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelSwitcher : MonoBehaviour
    {
    
        void Start()
        {
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
        }

        public void Switch(int num)
        {
            StartCoroutine(LoadLevel(num));
        }

        public IEnumerator LoadLevel(int num)
        {
            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(num);
            sceneLoad.allowSceneActivation = false;
            while (sceneLoad.progress<0.9f)
            {
                Debug.Log(sceneLoad.progress * 100 + "%");
                yield return null;
            }
            sceneLoad.allowSceneActivation = true;

        }
    }
}
