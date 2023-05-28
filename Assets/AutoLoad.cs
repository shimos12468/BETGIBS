using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoad : MonoBehaviour
{
   public void LoadFirstLevel()
    {
        SceneManager.LoadScene("lvl1");
    }
}
