using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.PlayerSpace
{
    public class ScoreKeeper : MonoBehaviour, ILevelCompleter
    {
        [SerializeField] int maxMultiplier = 3;
        [SerializeField] float multiplierTimer = 5.0f;
        [SerializeField] int maxStreak = 5;
        private int score = 0;
        private bool isMultiplying = false;
        private int scoreMultiplier = 0;
        private float currentTimer;
        private int streak = 0;
        private bool hasStreak = false;
        private GameObject spawner;
        public void IncreaseScore(int pointValue)
        {
            ApplyMultiplier(pointValue);
        }

        public int GetScore()
        {
            return score;
        }

        public int GetMultiplier()
        {
            return scoreMultiplier;
        }
        public void CompleteLevel()
        {
        
        }

        public void StageLevel()
        {
        
        }

        public void BreakStreak()
        {
            Debug.Log("ScoreKeeper broke streak");
            streak = 0;
            hasStreak = false;
        }

        public float GetMultiplierTimer()
        {
            return currentTimer;
        }
        public float GetTimerFraction()
        {
            return currentTimer/multiplierTimer;
        }
        public void SetStreak(int value)
        {
            streak += value;
            if (streak >= 3)
            {
                hasStreak = true;
            }

            if (streak > maxStreak)
            {
                streak = maxStreak;
            }
        }

        public int GetStreak()
        {
            return streak;
        }

        private void Start() 
        {
            spawner = GameObject.FindGameObjectWithTag("ScoreSpawner");
            currentTimer = 0.0f;
        }
        private void ApplyMultiplier(int pointValue)
        {
            if (!isMultiplying)
            {
                score += pointValue; 
                scoreMultiplier = 1;
                ReportScoreEvent(pointValue, scoreMultiplier);
                SetTimer();
                isMultiplying = true;

                return;
            }

            else if (isMultiplying)
            {
                SetMultiplier();
                int multipliedValue = pointValue * scoreMultiplier;
                score += multipliedValue; 
                ReportScoreEvent(multipliedValue, scoreMultiplier);
                SetTimer();
                return;
            }
        }

        private void ReportScoreEvent(int value, int multiplier)
        {
    
            var scoreReporter = spawner.GetComponent<IScoreReporter>();
            scoreReporter.ScorePoints(value, multiplier);
        }

        private void SetTimer()
        {
            currentTimer = multiplierTimer;
        }

        private void SetMultiplier()
        {
            if (scoreMultiplier <= 0)
                {
                    scoreMultiplier = 1;
                    return;
                }

            else if (scoreMultiplier < maxMultiplier && !hasStreak) 
            {
                scoreMultiplier++;
                return;
            }

            else if (scoreMultiplier < maxMultiplier && hasStreak)
            {
                scoreMultiplier++;
                scoreMultiplier = scoreMultiplier + streak;
                return;
            }
            
            
        }

        private void FixedUpdate() 
        {
            UpdateTimer();
            
        }

        private void UpdateTimer()
        {
            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            }
            if (currentTimer <= 0)
            {
                currentTimer = 0;
                isMultiplying = false;
                scoreMultiplier = 1;
            }
        }

        
    }
}