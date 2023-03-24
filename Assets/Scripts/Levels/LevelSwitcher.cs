using System.Collections;
using System.Security.Cryptography;
using Consumables;
using Events;
using Game;
using UI;
using UI.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventType = Events.EventType;

namespace Levels
{
    public class LevelSwitcher : MonoBehaviour
    {
        public static LevelSwitcher Switcher;
        void Start()
        {
            Switcher = this;
            Application.targetFrameRate = 60;
            Consumable.Add(Consumable.ConsumableType.Bomb, 7);
            Consumable.Add(Consumable.ConsumableType.Poison, 4);
            DontDestroyOnLoad(gameObject);
        }

        public void Switch(int num)
        {
            StartCoroutine(LoadLevel(num));
        }

        public IEnumerator LoadLevel(int num)
        {
            LoadingScreen screen = FindObjectOfType<LoadingScreen>();
           
            yield return screen.StartCoroutine("ShowAnimation");
            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(num);
            sceneLoad.allowSceneActivation = false;
            while (sceneLoad.progress<0.9f)
            {
                yield return null;
            }
            LoadingScreen.Fade = true;
            sceneLoad.allowSceneActivation = true;
            yield return new WaitUntil(() => sceneLoad.isDone);
            screen = FindObjectOfType<LoadingScreen>();
            screen.StartCoroutine("CloseAnimation");

        }
    }
}
