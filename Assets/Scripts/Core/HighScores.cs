using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demo.SceneManagement;
using UnityEngine;

namespace Demo.Core
{
    [System.Serializable]
        public class HighScore
        {
            
            private string _name;
            private int _score;
            public string Name {get{return _name;}
                                set {_name = value;}
                                }
            public int Score {get{return _score;}
                            set {_score = value;}
                            }
        }
    public class HighScores : MonoBehaviour, ISaveable
    {
        //Component is on the Player
        [SerializeField] int _maxListSize = 10;
        private List<HighScore> _highScores = new List<HighScore>();
        private HighScore _latestScore = null;
        private SavingWrapper _savingWrapper;
        private bool isHighScore = false;
        public event Action OnHighScore;

        private void Start()
        {
            //Debug.Log("High Scores before setup " + _highScores.Count());
            _highScores = _highScores.OrderBy(pos => pos.Score).ToList();
            _highScores.Reverse();
            //Debug.Log("High Scores after setup " + _highScores.Count());
            _latestScore = new HighScore();

        }
        public void SubmitScore(string name, int score)
        {
            Debug.Log("SubmitScore Recieveed name of: " + name);
            
            _latestScore = new HighScore();
            _latestScore.Name = name;
            _latestScore.Score = score;
            

            if (_highScores.Count() < _maxListSize || score > GetLowestScore().Score)
            {
                AddLatestScore();
                ManageScoreList();
            }
            
            
        }

        public HighScore GetLatestScore()
        {

            return _latestScore;
        }
        
        public bool GetIsHighScore()
        {
            return isHighScore;
        }

        public List<HighScore>GetHighScores()
        {
            return _highScores;
        }

        private HighScore GetLowestScore()
        {
            HighScore lowestScore = new HighScore();
            if (_highScores.Count() > 1)
            {
                int lowestValue = _highScores.Min(score => score.Score);
                
                foreach (HighScore score in _highScores)
                {
                    if (score.Score == lowestValue)
                    {
                        lowestScore = score;
                        return lowestScore;
                    }
                }
                Debug.Log("Lowest Score: " + lowestValue);
                lowestScore.Name = "???";
                lowestScore.Score = lowestValue;
                return lowestScore;
            }

            // returns default score of 0 if no scores are on the list
            lowestScore.Name = "Lowest Score";
            lowestScore.Score = 0;
            return lowestScore;
        }

        private void AddLatestScore()
        {
            Debug.Log("Adding Score");
            isHighScore = true;
            _highScores.Add(_latestScore);
        }

        private void ManageScoreList()
        {
            Debug.Log("Managing Score list");
            if (_highScores.Count > _maxListSize)
            {
                _highScores.Remove(GetLowestScore());
            }

            if (OnHighScore != null)
            {
                Debug.Log("OnhighScore");
                OnHighScore();
            }
        }

        
        public object CaptureState()
        {
            List<HighScore> saveState = new List<HighScore>();
            foreach (HighScore score in _highScores)
            {
                saveState.Add(score);
            }
            return saveState;
        }

        public void RestoreState(object state)
        {
            List<HighScore> restoreState = state as List<HighScore>;
            _highScores.Clear();
            foreach (HighScore score in restoreState)
            {
                _highScores.Add(score);
            }
        }
    }
}


