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

        private void AddNewScore()
        {
            GameObject scoreInstance = Instantiate(scorePrefab, transform.position, Quaternion.identity, scoresParent);
            DrawScores();
            
        }
        public void DrawScores()
        {
            Debug.Log("Drawing Scores");
            foreach (HighScore score in _highScores.GetHighScores())
            {
                Instantiate(scorePrefab,transform.position, Quaternion.identity, scoresParent);
            }
            StartCoroutine(Fade());
            
        }

        private IEnumerator Fade()
        {
            Debug.Log("Fading in and out hopefully");
            yield return new WaitForEndOfFrame();
            _panelFader.FadeInOutScore();
        }

    }
}
