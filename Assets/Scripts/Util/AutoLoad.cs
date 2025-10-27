using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoLoad : MonoBehaviour
{
    public string NextLevel;
    public Button settings;
    public Button pause;

    public static AutoLoad instance;


    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        

        settings.onClick.AddListener(OpenSettings);
        settings.gameObject.SetActive(false);

        pause.onClick.AddListener(OpenPauseMenu);
        pause.gameObject.SetActive(false);

    }

    public async void LoadFirstLevel()
    {
        await SceneManager.LoadSceneAsync(NextLevel);
        settings.gameObject.SetActive(true);
        pause.gameObject.SetActive(true);

    }

    public void OpenSettings()
    {
        GameObject.FindGameObjectWithTag("Settings").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        GameObject.FindGameObjectWithTag("Settings").transform.GetChild(1).gameObject.SetActive(true);
    }

    public void LoadNextLevel(string nextLevelName)
    {
        if (nextLevelName == "Menu")
        {
            settings.gameObject.SetActive(false);
            pause.gameObject.SetActive(false);
        }
        SceneManager.LoadSceneAsync(nextLevelName);
    }
}
