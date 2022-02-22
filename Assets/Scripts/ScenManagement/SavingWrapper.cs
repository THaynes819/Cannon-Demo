using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using Demo.Saving;

namespace Demo.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string _currentSaveKey = "currentSaveName";
        [SerializeField] float fadeintime = 0.5f;
        [SerializeField] float fadeOutTime = 0.2f;
        [SerializeField] int levelOneScene = 1;
        [SerializeField] int mainMenuScene = 0;
        private string _playerName;
        private List<string> allSaveFiles = new List<string>();
        public event Action OnNamingError;

        public void ContinueGame()
        {
            if (!PlayerPrefs.HasKey(_currentSaveKey)) return;
            if (!GetComponent<SavingManager>().SaveFileExists(GetCurrentSave())) return;
            StartCoroutine (LoadLastScene());
        }

        public void CreateNewGame(string saveFile)
        {
            //Debug.Log("Create New Game Called");
            //Debug.Log("Savefile " + saveFile);

            // bool is to prevent bug that was naming the file twice
            bool hasNamed = false;
            if (String.IsNullOrEmpty(saveFile) && OnNamingError != null) //Create New doesn't save in level one until first save after starting. Need to fix. Maybe fixed.
            {
                OnNamingError();
            }

            if (!String.IsNullOrEmpty(saveFile) && hasNamed == false)
            {
                hasNamed = true;
                SetCurrentSave(saveFile);
                _playerName = saveFile;
                
                StartCoroutine(LoadScene (saveFile, levelOneScene));
            }
        }

            public void LoadMenu()
        {
            StartCoroutine(LoadMenuScene());
        }

        public void LoadGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            ContinueGame();
        }

        private void SetCurrentSave(string saveFile)
        {  
            //Debug.Log("SetCurrentSave set to " + saveFile);              
            PlayerPrefs.SetString(_currentSaveKey, saveFile);
        }

        public string GetPlayerName()
        {
            return _playerName;
        }
        public string GetCurrentSave()
        {
            return PlayerPrefs.GetString(_currentSaveKey);
        }
        
        public int GetMainMenuScene()
        {
            return mainMenuScene;
        }

        private IEnumerator LoadMenuScene()
        {
            SceneFader fader = FindObjectOfType<SceneFader>();
            yield return fader.FadeOut(fadeOutTime);            
            yield return SceneManager.LoadSceneAsync (mainMenuScene);
            yield return fader.FadeIn(fadeintime);
        }

        private IEnumerator LoadScene (string saveFile, int scene)
        {
            //Debug.Log("Loading Scene " + scene.ToString());
            SceneFader fader = FindObjectOfType<SceneFader>();            
            yield return fader.FadeOut(fadeOutTime);  
            yield return SceneManager.LoadSceneAsync (scene);
            GetComponent<SavingManager>().Save(saveFile);
            yield return fader.FadeIn(fadeintime);
            
        }

        IEnumerator LoadLastScene()
        {
            SceneFader fader = FindObjectOfType<SceneFader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return GetComponent<SavingManager>().LoadLastScene(GetCurrentSave());      
            
            yield return fader.FadeIn(fadeintime);
        }

        public void Save()
        {
            GetComponent<SavingManager>().Save(GetCurrentSave());
        }

        public void Load()
        {
            GetComponent<SavingManager>().Load(GetCurrentSave());
        }

        public void Delete()
        {
            GetComponent<SavingManager>().Delete(GetCurrentSave());
        }

        public IEnumerable<string> ListSaves()
        {
            if (GetComponent<SavingManager>().ListSaves() != null)
            {
                return GetComponent<SavingManager>().ListSaves();
            }
            else
            {
                Debug.Log("Saving Wrapper Reports List of Saves as null");
                return null;
            }
            
        }
    }
}

