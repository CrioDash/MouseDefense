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
            Application.targetFrameRate = 120;
            DontDestroyOnLoad(gameObject);
        }

        public void Switch(int num)
        {
            StartCoroutine(LoadLevel(num));
        }

        public IEnumerator LoadLevel(int num)
        {
            SceneManager.LoadScene(num);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == num);
            yield return new WaitForSeconds(1f);
        }
    }
}
