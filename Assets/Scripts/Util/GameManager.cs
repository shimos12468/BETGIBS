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
        AutoLoad.instance.LoadNextLevel(NextLevelName);

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
            Invoke("ResetLevel", 2.0f);
        }
    }

    public void ResetLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
