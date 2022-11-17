using System.Collections;
using Events;
using UI;
using UI.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventType = Events.EventType;

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
            Debug.Log(Time.time);
            while (sceneLoad.progress<0.9f)
            {
                Debug.Log(sceneLoad.progress * 100 + "%");
                yield return null;
            }
            yield return new WaitUntil(() => sceneLoad.isDone);
        }
    }
}
