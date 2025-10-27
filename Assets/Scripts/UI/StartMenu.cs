using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        AutoLoad.instance.LoadFirstLevel();
    }
    public void OpenSettings()
    {
        AutoLoad.instance.OpenSettings();
    }
    public void QuiteGame()
    {
        print("Quit Game");

        Application.Quit();
    }
}
