using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoad : MonoBehaviour
{
    public string NextLevel;
   public void LoadFirstLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }
}
