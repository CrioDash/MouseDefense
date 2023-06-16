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
            Application.targetFrameRate = int.MaxValue;
            DontDestroyOnLoad(gameObject);
        }

        //Удалить сохранения
        public void DeleteSave()
        {
            SaveSystem.DeleteSave();
            SaveSystem.Load();
            Switch(SceneManager.GetActiveScene().buildIndex);
        }
        

        
        public void Switch(int num)
        {
            SaveSystem.Save();
            Switcher.StartCoroutine(LoadLevel(num));
        }

        //Переключить сцену
        public IEnumerator LoadLevel(int num)
        {
            LoadingScreen.Instance.Fade = true;
            yield return LoadingScreen.Instance.StartCoroutine("ShowAnimation");
            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(num);
            sceneLoad.allowSceneActivation = false;
            SaveSystem.Save();
            while (sceneLoad.progress<0.9f)
            {
                yield return null;
            }
            
            sceneLoad.allowSceneActivation = true;
            yield return new WaitUntil(() => sceneLoad.isDone);
            yield return LoadingScreen.Instance.StartCoroutine("CloseAnimation");
            if(Level.Instance!=null) 
                EventBus.Publish(EventType.START);
        }
    }
}
