using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(num);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == num);
        Level level = FindObjectOfType<Level>();
        yield return new WaitForSeconds(1f);
        
    }
}
