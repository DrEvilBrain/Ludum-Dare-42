﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public Button start;

    void Start()
    {
        start.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
