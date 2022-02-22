using System.Collections;
using System.Collections.Generic;
using Demo.Control;
using Demo.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField nameInput = null;
        [SerializeField] Button startButton = null;
        [SerializeField] Button closeButton = null;
        [SerializeField] Button highScoreButton = null;
        [SerializeField] GameObject errorPanel = null;

        private SavingWrapper _savingWrapper;
        private PlayerController _playerController;
        private ScoreKeeper _scoreKeeper;
        private void Start() 
        {
            _savingWrapper = FindObjectOfType<SavingWrapper>();
            errorPanel.SetActive(false);
            _savingWrapper.OnNamingError += NamingError;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _playerController = player.GetComponent<PlayerController>();
            _scoreKeeper = player.GetComponent<ScoreKeeper>();
            
            startButton.onClick.AddListener(() =>
            {
                StartGame();
            });

            closeButton.onClick.AddListener(() =>
            {
                CloseGame();
            });
            
            
        }

        public void StartGame()
        {
            Debug.Log("StartGame Pressed");
            //_scoreKeeper.SetName(nameInput.text);
            _savingWrapper.CreateNewGame(nameInput.text);
        }

        public void CloseGame()
        {
            Application.Quit();
        }

        public void CloseError()
        {
            errorPanel.SetActive(false);
        }

        private void NamingError()
        {
            errorPanel.SetActive(true);
        }

    }
}

