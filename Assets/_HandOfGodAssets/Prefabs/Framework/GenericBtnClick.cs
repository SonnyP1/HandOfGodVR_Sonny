using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu]
public class GenericBtnClick : ScriptableObject
{
    public static void RestartClick()
    {
        SceneManager.LoadScene("LevelOne",LoadSceneMode.Single);
    }

    public static void QuitGameClick()
    {
        Application.Quit();
    }
}
