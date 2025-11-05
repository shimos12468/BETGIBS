using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        //public IEnumerator LoadLastScene(string saveFile)
        //{
        //    Dictionary<string, Dictionary<string, object>> state = LoadFile(saveFile);
        //    int buildIndex = SceneManager.GetActiveScene().buildIndex;
        //    if (state.ContainsKey("lastSceneBuildIndex"))
        //    {
        //        buildIndex = (int)state["lastSceneBuildIndex"];
        //    }
        //    yield return SceneManager.LoadSceneAsync(buildIndex);
        //    RestoreState(state);
        //}

        public void Save(string saveFile)
        {
            Dictionary<string, Dictionary<string, object>> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        public void Delete(string saveFile)
        {
            File.Delete(GetPathFromSaveFile(saveFile));
        }

        private Dictionary<string, Dictionary<string, object>> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("loading from " + path);
            if (!File.Exists(path))
            {
                print("hi new file");
                return new Dictionary<string, Dictionary<string, object>>();
            }
            string data= File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(data);
        }

        private void SaveFile(string saveFile, object state)
        {

            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path); 
            string data= JsonConvert.SerializeObject(state);
            File.WriteAllText(path, data);
        }

        private void CaptureState(Dictionary<string, Dictionary<string, object>> state)
        {
            print("man WTF");
            foreach (SaveableEntity saveable in FindObjectsByType<SaveableEntity>(FindObjectsSortMode.None))
            {
                print(saveable.name);
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            //state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, Dictionary<string, object>> state)
        {
            foreach (SaveableEntity saveable in FindObjectsByType<SaveableEntity>(FindObjectsSortMode.None))
            {
                string id = saveable.GetUniqueIdentifier();

                print(saveable.name);
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            print(Application.persistentDataPath);
            return Path.Combine(Application.persistentDataPath, saveFile + ".json");
        }
    }
}