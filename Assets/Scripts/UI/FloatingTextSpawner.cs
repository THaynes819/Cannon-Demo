using System;
using Demo.Control;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class FloatingTextSpawner : MonoBehaviour, IScoreReporter
    {
        [SerializeField] ScoreText baseScorePrefab = null;
        [SerializeField] ScoreText twoXScorePrefab = null;
        [SerializeField] ScoreText threeXScorePrefab = null;
        private Color textColor;

        public void ScorePoints(int score, int multiplier, int streak)
        {
            Spawn(score, multiplier, streak);
        }

        public void Spawn (int score, int multiplier, int streak)
		{

            ScoreText scoreTextInstance;

            //ScoreText textPrefeb = new ScoreText();
            score = score * multiplier;
            
            if (multiplier == 1)
            {
                scoreTextInstance = Instantiate<ScoreText> (baseScorePrefab, transform);
                scoreTextInstance.SetValue (score);
            }
            if (multiplier == 2)
            {
                
                scoreTextInstance = Instantiate<ScoreText> (twoXScorePrefab, transform);
                scoreTextInstance.SetValue (score);
                
            }
            if (multiplier >= 3)
            {
                scoreTextInstance = Instantiate<ScoreText> (threeXScorePrefab, transform);
                scoreTextInstance.SetValue (score);
            }

            
		}

        
    }
}