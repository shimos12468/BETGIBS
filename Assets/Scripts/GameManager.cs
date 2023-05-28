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


    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null) _instance = this;
        else { Destroy(gameObject); }
    }

    void Start()
    {
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

    public void CheckWin() {
        bool won = true;
        foreach (var ragdoll in ragdolls)
        {
            if (ragdoll.IsBroken()) won = false;
        }
        if (won) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextLevelName);
        }
        else {
            ResetLevel();
        }
    }

    public void ResetLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
