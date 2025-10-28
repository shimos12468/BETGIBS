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

    [Serializable]
    public struct LevelData
    {
        public Level levelSO;
        public string levelName;
    }

    public List<LevelData> levelsData;
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

    internal void LoadLevelByKey(string levelName)
    {
        print("Loading level: " + levelName);
        LevelData levelToLoad = new LevelData();

        for (int i = 0; i < levelsData.Count; i++)
        {
            if (levelName == levelsData[i].levelSO.levelkey)
            {
                levelToLoad = levelsData[i];
                break;
            }
        }


        int level = levelToLoad.levelSO.currentIndex;

        if(level>= levelToLoad.levelSO.levels.Count)
        {
            level = 0;
            levelToLoad.levelSO.currentIndex = 0;
        }

        SceneManager.LoadSceneAsync(levelToLoad.levelSO.levels[level]);
        PlayerPrefs.SetString("LevelName", levelName);

    }

    public void LoadNextLevel()
    {
        print("Loading level by key");
        string key= PlayerPrefs.GetString("LevelName");

        for(int i = 0; i < levelsData.Count; i++)
        {
            if (key == levelsData[i].levelSO.levelkey)
            {
                levelsData[i].levelSO.currentIndex++;
                break;
            }
        }

        LoadLevelByKey(key);


    }
}
