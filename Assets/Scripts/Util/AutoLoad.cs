using Newtonsoft.Json;
using RPG.Saving;
using RPG.SceneManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AutoLoad : MonoBehaviour , ISaveable
{
    public string NextLevel;
    public Button settingsButton;
    public Button pauseButon;
    public Slider HealthSlider;
    public static AutoLoad instance;
    
    public int maxHealth = 3;
    bool canPlay = true;



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


        GetComponent<SavingWrapper>().Load();
    }
    public void Start()
    {
        DontDestroyOnLoad(gameObject);


        settingsButton.onClick.AddListener(OpenSettings);
        settingsButton.gameObject.SetActive(false);

        pauseButon.onClick.AddListener(OpenPauseMenu);
        pauseButon.gameObject.SetActive(false);

        SetupHealth();


    }

    private void SetupHealth()
    {
        HealthSlider.gameObject.SetActive(false);

        HealthSlider.maxValue = maxHealth;

        //HealthSlider.value = PlayerPrefs.GetInt("PlayerHealth", maxHealth);

        //canPlay = PlayerPrefs.GetInt("CanPlay", 1) == 1 ? true : false;
    }

    public async void LoadFirstLevel()
    {

        if (!canPlay)
        {
            return;
        }
        if (canPlay)
        {
            await SceneManager.LoadSceneAsync(NextLevel);
            settingsButton.gameObject.SetActive(true);
            pauseButon.gameObject.SetActive(true);
            HealthSlider.gameObject.SetActive(true);


        }

        
        

    }
    private void Update()
    {
        if (!canPlay)
        {

            //check scene

            if(SceneManager.GetActiveScene().name != "Menu")
            { 
                return;
            }
            
            //make precentage show but I am not sure how to do that yet
            GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>().interactable = false;
            
            
            string untilTime = PlayerPrefs.GetString("HealthUntil");
            DateTime current = DateTime.UtcNow;
            print("Current time: " + current.ToString());
            DateTime until = DateTime.Parse(untilTime);
            print("Until time: " + until.ToString());

            if (current < until)
            {
                print("not yet");
            }
            else
            {
                print("can play now");
                canPlay = true;
                PlayerPrefs.DeleteKey("HealthUntil");
                GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>().interactable = true;
            }


        }

       

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

            HealthSlider.gameObject.SetActive(false);
            settingsButton.gameObject.SetActive(false);
            pauseButon.gameObject.SetActive(false);
        }
        SceneManager.LoadSceneAsync(nextLevelName);
    }

   
    public void LoadLevelByKey(string levelName)
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

        int level = GetNextLevelIndex(levelToLoad);

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

        LevelComplete();
        LoadLevelByKey(key);


    }

    internal void PlayerLost(string v)
    {
        DateTime until = DateTime.UtcNow.Add(TimeSpan.FromMinutes(0.1));

        PlayerPrefs.SetString("HealthUntil", until.ToString());
        canPlay = false;
        HealthSlider.value = maxHealth;
        GetComponent<SavingWrapper>().Save();
        LoadNextLevel(v);
    }



    internal void HealthLost(int health)
    {
       HealthSlider.value = health;
    }

  

    private static int GetNextLevelIndex(LevelData levelToLoad)
    {
        int level = levelToLoad.levelSO.currentIndex;

        if (level >= levelToLoad.levelSO.levels.Count)
        {
            level = 0;
            levelToLoad.levelSO.currentIndex = 0;
        }

        return level;
    }


    [Serializable]
    struct SaveData
    {
        public List<LevelData> Levels;
        public bool canplay;
        public int currentHealth;
    }


    public object CaptureState()
    {

        SaveData data = new SaveData();
        data.Levels = levelsData;
        data.canplay = canPlay;
        data.currentHealth = (int)HealthSlider.value;

        string d = JsonConvert.SerializeObject(data);

        return d;

    }

    public void RestoreState(object state)
    {
        SaveData data= JsonConvert.DeserializeObject<SaveData>((string)state);
        
        levelsData = data.Levels;
        canPlay = data.canplay;
        HealthSlider.value = data.currentHealth;

        print("Restored state");
        print("Can play: " + canPlay);
        print("Current Health: " + HealthSlider.value);
        print("Levels count: " + levelsData.Count);
        print("First level index: " + levelsData[0].levelSO.currentIndex);
    }


    private void OnApplicationQuit()
    {

        GetComponent<SavingWrapper>().Save();
    }

    internal void LevelComplete()
    {
        GetComponent<SavingWrapper>().Save();
    }
}
