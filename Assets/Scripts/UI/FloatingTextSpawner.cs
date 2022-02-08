using System;
using Demo.PlayerSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class FloatingTextSpawner : MonoBehaviour, IScoreReporter
    {
        [SerializeField] ScoreText scoreTextPrefab = null;
        private Color textColor;

        public void ScorePoints(int score, int multiplier)
        {
            Debug.Log("Spawner Scored " + score + " x " + multiplier );
            Spawn(score, multiplier);
        }

        public void Spawn (int score, int multiplier)
		{
            
            if (multiplier == 1)
            {
                textColor = Color.cyan;
            }
            if (multiplier == 2)
            {
                textColor = Color.yellow;
            }
            if (multiplier == 3)
            {
                textColor = Color.red;
            }

            ScoreText scoreTextInstance = Instantiate<ScoreText> (scoreTextPrefab, transform);
            scoreTextInstance.SetValue(score, textColor);
		}
    }
}