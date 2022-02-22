using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Demo.Control;
using Demo.Core;

namespace Demo.UI
{
    public class HighScoresUI : MonoBehaviour
    {
        [SerializeField] GameObject scorePrefab = null;
        [SerializeField] Transform scoresParent = null;
        [SerializeField] float checkDelay = 0.1f;
        private GameObject _player;
        private ScoreKeeper _scoreKeeper;
        private HighScores _highScores;
        private UIPanelFader _panelFader;
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _scoreKeeper = _player.GetComponent<ScoreKeeper>();
            _highScores = _player.GetComponent<HighScores>();
            _highScores.OnHighScore += AddNewScore;
            _panelFader = GetComponent<UIPanelFader>();
        }

        public void DrawScores()
        {
            Debug.Log("Drawing Scores");
            foreach (HighScore score in _highScores.GetHighScores())
            {
                Instantiate(scorePrefab,transform.position, Quaternion.identity, scoresParent);
            }
            _panelFader.ShowScores();
        }

        private void AddNewScore()
        {
            Debug.Log("AddNewScore Called. Instantiating");
            GameObject scoreInstance = Instantiate(scorePrefab, transform.position, Quaternion.identity, scoresParent);
            Debug.Log("AddNewScore Drawing");
            DrawScores();
            
        }

    }
}
