using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameplayStatics
{
    public static Transform GetWalkmanTransform()
    {
        if(GetWalkMan())
        {
            return GetWalkMan().transform;
        }
        return null;
    }
    public static WalkMan GetWalkMan()
    {
        if(walkMan == null)
        {
            walkMan = GameObject.FindObjectOfType<WalkMan>();
        }
        return walkMan;
    }
    static WalkMan walkMan;

    public static Player GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindObjectOfType<Player>();
        }
        return player;
    }
    static Player player;
    public static Earth GetEarth()
    {
        if (earth == null)
        {
            earth = GameObject.FindObjectOfType<Earth>();
        }
        return earth;
    }
    static Earth earth;





    public static void LoadScene(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public static void CloseApplication()
    {
        Application.Quit();
    }
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }
    public static void UnPauseGame()
    {
        Time.timeScale = 1;
    }
}
