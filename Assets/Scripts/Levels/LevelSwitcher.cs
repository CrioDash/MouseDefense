using System;
using System.Collections;
using System.Security.Cryptography;
using Consumables;
using Events;
using Game;
using GameData;
using UI;
using UI.Pause;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using EventType = Events.EventType;

namespace Levels
{
    public class LevelSwitcher : MonoBehaviour
    {
        public static LevelSwitcher Switcher;

        private void Awake()
        {
            SaveSystem.Load();
            if (Switcher != null)
                Destroy(Switcher.gameObject);
            Switcher = this;
            Application.targetFrameRate = 120;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            
        }

        public void DeleteSave()
        {
            SaveSystem.DeleteSave();
            SaveSystem.Load();
        }

        public void Switch(int num)
        {
            SaveSystem.Save();
            Switcher.StartCoroutine(LoadLevel(num));
        }

        public IEnumerator LoadLevel(int num)
        {
            yield return LoadingScreen.Instance.StartCoroutine("ShowAnimation");
            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(num);
            sceneLoad.allowSceneActivation = false;
            SaveSystem.Save();
            while (sceneLoad.progress<0.9f)
            {
                yield return null;
            }
            LoadingScreen.Instance.Fade = true;
            Resources.LoadAll("Prefab");
            sceneLoad.allowSceneActivation = true;
            yield return new WaitUntil(() => sceneLoad.isDone);
            EventBus.Publish(EventType.PAUSE);
            yield return new WaitForSecondsRealtime(1f);
            LoadingScreen.Instance.StartCoroutine("CloseAnimation");
            EventBus.Publish(EventType.START);
        }
    }
}
