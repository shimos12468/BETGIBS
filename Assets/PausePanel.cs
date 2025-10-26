using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
   



    public void GoToStartScene()
    {
        foreach(Transform g in gameObject.transform.parent)
        {
            g.gameObject.SetActive(false);
        }

        SceneManager.LoadSceneAsync("Menu");

        
    }

    public void QuitGame()
    {
    }

}
