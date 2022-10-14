using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitLevelScript : MonoBehaviour
{
    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }
}
