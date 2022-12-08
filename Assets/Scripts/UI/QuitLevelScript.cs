using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitLevelScript : MonoBehaviour
{
    public void QuitLevel()
    {
        LevelSwitcher.Switcher.Switch(0);
    }
}
