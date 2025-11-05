using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defualtSavingFile = "ewqeqw";
        [SerializeField] float fadeInTime = 0.3f;

        //IEnumerator Start()
        //{
            
        //    //FadeScreen fader = FindAnyObjectByType<FadeScreen>();
        //    //fader.FadeInInit();
        //    yield return GetComponent<SavingSystem>().LoadLastScene(defualtSavingFile);
        //    //fader.FadeIn();
        //}
        void Update()
        {
          
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defualtSavingFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defualtSavingFile);
        }
    }

}