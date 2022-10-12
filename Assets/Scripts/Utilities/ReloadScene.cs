using System.Collections;
using System.Collections.Generic;
using UI.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ButtonPause))]
public class ReloadScene : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
