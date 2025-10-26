using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        AutoLoad.instance.LoadFirstLevel();
    }
}
