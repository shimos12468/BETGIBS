using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public BowBehaviour[] bows;
    public BarrelBehaviour[] barrels;
    public RagdollController[] ragdolls;
    public string NextLevelName;
    public float levelClearTime = 5f;
    public GameObject PassedText;
    public GameObject FailedText;

    int health;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null) _instance = this;
        else { Destroy(gameObject); }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    void Start()
    {
        health= (int)FindObjectOfType<AutoLoad>().HealthSlider.value;
        bows = GameObject.FindObjectsOfType<BowBehaviour>();
        barrels = GameObject.FindObjectsOfType<BarrelBehaviour>();
        ragdolls = GameObject.FindObjectsOfType<RagdollController>();


        EnableDrawMode();
    }

    

    public void EnableDrawMode() {
        DoodleController.instance.ResetDoodle();
        DoodleController.instance.DisableDoodlePhysics();
        DoodleController.instance.ShowDrawingAreas();
    }

    public void Play() {
        DoodleController.instance.HideDrawingAreas();
        DoodleController.instance.EnableDoodlePhysics();
        foreach (var bow in bows)
        {
            bow.StartShooting();
        }
        foreach (var ragdoll in ragdolls)
        {
            ragdoll.EnableRagdollPhysics();
        }
        Invoke("CheckWin", 5f);
    }

    public void LoadNextLevel()
    {
        AutoLoad.instance.LoadNextLevel();

        //UnityEngine.SceneManagement.SceneManager.LoadScene(NextLevelName);
    }

    public void CheckWin() {
        bool won = true;
        foreach (var ragdoll in ragdolls)
        {
            if (ragdoll.IsBroken()) won = false;
        }
        if (won) {
            PassedText.SetActive(true);
            Debug.Log("Herer");

            Invoke("LoadNextLevel", 2.0f);
        }
        else {
            FailedText.SetActive(true);
            Debug.Log("Herer");

            health--;

            //invoke health event here
            AutoLoad.instance.HealthLost(health);
            if (health > 0)
            {
                Invoke("ResetLevel", 2.0f);
            }
            else
            {
                Invoke("GoToMainMenu", 2.0f);
            }
            //should check health and if is less than zero or zero go to main menu and add timer 
        }
    }

    

    public void ResetLevel() {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        AutoLoad.instance.LoadNextLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {

        AutoLoad.instance.PlayerLost("Menu");
    }

}
