using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public static Scenemanager Instance;

    private void Start()
    {
        Instance = this;   
    }

    public void WinScene()
    {
        Instance = this;
        SceneManager.LoadScene("WinScene");
    }

    public void MainScene()
    {
        Instance = this;
        SceneManager.LoadScene("SampleScene");
    }
    public void LoseScene()
    {
        Instance = this;
        SceneManager.LoadScene("LoseScene");
    }
}
