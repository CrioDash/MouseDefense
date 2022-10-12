using System.Collections;
using System.Collections.Generic;
using UI.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ButtonPause))]
public class ToMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
