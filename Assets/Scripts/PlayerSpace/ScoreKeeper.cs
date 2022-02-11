using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.PlayerSpace
{
    public class ScoreKeeper : MonoBehaviour, ILevelCompleter
    {
        [SerializeField] int startingMaxMultiplier = 3;
        [SerializeField] float multiplierTimer = 5.0f;
        [SerializeField] int maxStreak = 5;
        private int _maxMultiplier;
        private int _score = 0;
        private int _scoreMultiplier = 1;
        private int _streak = 0;
        private int _targetBonus;
        private bool _isMultiplying = false;
        private bool _hasHalfBonus = false;
        private float _currentTimer;
        private bool _hasStreak = false;
        private GameObject _spawner;

        //recieves score increases from other objects
        public void IncreaseScore(int pointValue)
        {
            ApplyMultiplier(pointValue);
        }

        public int GetScore()
        {
            return _score;
        }

        public int GetMultiplier()
        {
            return _scoreMultiplier;
        }

        public bool GetHasBonus()
        {
            return _hasHalfBonus;
        }
        public void CompleteLevel()
        {
        
        }

        public void StageLevel()
        {
        
        }

        public void HalfWayBonus()
        {
            //Debug.Log("Score Keeper Half"); Working 2/11/2022
            if (_streak >= maxStreak)
            {
                _hasHalfBonus = true;
                Debug.Log("Setting halfBonus to True: " + _hasHalfBonus);
            }
            
        }

        //informs this object that the streak is broken and sets the streak to zero
        public void BreakStreak()
        {
            _streak = 0;
            _hasStreak = false;
        }

        public float GetMultiplierTimer()
        {
            return _currentTimer;
        }
        public float GetTimerFraction()
        {
            return _currentTimer/multiplierTimer;
        }

        //each increases the streak to increase the score 
        public void SetStreak(int value)
        {
            _streak += value;
            if (_streak >= 2)
            {
                _hasStreak = true;
            }

            if (_streak > maxStreak)
            {
                _streak = maxStreak;
            }
        }

        public int GetStreak()
        {
            if (_hasStreak)
            {
                return _streak;
            }
            else
            {
                return 1;
            }
        }

        private void Start() 
        {
            _scoreMultiplier = 1;
            _maxMultiplier = startingMaxMultiplier;
            _spawner = GameObject.FindGameObjectWithTag("ScoreSpawner");
            _currentTimer = 0.0f;
        }
        private void ApplyMultiplier(int pointValue)
        {
            if (!_isMultiplying)
            {
                SetMultiplier();
                Debug.Log("Should be 1: Multiplier is " + _scoreMultiplier);
                _streak = 0;
                _score += pointValue; 
                ReportScoreEvent(pointValue, _scoreMultiplier, _streak);
                SetTimer();
                _isMultiplying = true;
                Debug.Log("Wasn't multiplying. Multiplier is " + _scoreMultiplier);
                return;
            }

            else if (_isMultiplying)
            {
                Debug.Log(" Multiplier before SetMultiplier is " + _scoreMultiplier);
                SetMultiplier();
                Debug.Log("Should be >1 Multiplier is " + _scoreMultiplier);
                int multipliedValue = pointValue * (_scoreMultiplier + _streak);
                Debug.Log("Was multiplying. Multiplier is " + _scoreMultiplier);
                // score is increased at this point
                _score += multipliedValue; 
                ReportScoreEvent(pointValue, _scoreMultiplier, _streak);
                SetTimer();
                return;
            }
        }

        private void ReportScoreEvent(int value, int multiplier, int streak)
        {
            var scoreReporter = _spawner.GetComponent<IScoreReporter>();
            scoreReporter.ScorePoints(value, multiplier, streak);
        }

        private void SetTimer()
        {
            _currentTimer = multiplierTimer;
        }

        private void SetMultiplier()
        {
            Debug.Log("Multiplier at start of SetMultiplier: " + _scoreMultiplier);

            if (_hasHalfBonus)
            {
                //Debug.Log("half bonus is " + _hasHalfBonus); Working 2/11/2022
                _maxMultiplier = startingMaxMultiplier + 1;
                Debug.Log("Max X: " + _maxMultiplier);
            }
            if (!_hasHalfBonus)
            {
                _maxMultiplier = startingMaxMultiplier;
            }

            //Applies the multiplier with each succful hit up to the max
            if (_scoreMultiplier < _maxMultiplier)
            {
                Debug.Log("Should be >1: Multiplier is " + _scoreMultiplier);
                _scoreMultiplier++;
                return;
            }
            
            if (!_hasStreak)
            {
                _scoreMultiplier = 1;
            }

            // ensures multiplier is never less than 0
            if (_scoreMultiplier <= 0) //removed !hasStreak condition to fix error
                {
                    Debug.Log("Error: Multiplier was 0. set to 1: " + _scoreMultiplier);
                    _hasHalfBonus = false;
                    _scoreMultiplier = 1;
                    return;
                }

                return;
        }

        private void FixedUpdate() 
        {
            UpdateTimer();
            
        }

        private void UpdateTimer()
        {
            if (_currentTimer > 0)
            {
                _currentTimer -= Time.deltaTime;
            }
            if (_currentTimer <= 0 && _isMultiplying)
            {
                _currentTimer = 0;
                _isMultiplying = false;
                _scoreMultiplier = 1;
                Debug.Log("Timer hit 0. Multiplier is " + _scoreMultiplier);
            }
        }
    }
}