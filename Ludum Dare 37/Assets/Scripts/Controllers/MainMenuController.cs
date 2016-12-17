﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void OnPlay()
    {
        SceneManager.LoadSceneAsync(1);
    }


    public void OnExit()
    {
        Application.Quit();
    }

}
