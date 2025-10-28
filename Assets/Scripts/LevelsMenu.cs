using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    public Button level;

    private void Awake()
    {
        level.onClick.AddListener(() => {
            AutoLoad.instance.LoadLevelByKey(level.GetComponent<LevelButton>().levelName);
        });
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
