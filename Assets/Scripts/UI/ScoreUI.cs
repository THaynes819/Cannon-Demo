using System.Collections;
using System.Collections.Generic;
using Demo.PlayerSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class ScoreUI : MonoBehaviour, ILevelCompleter
    {
        [SerializeField] TextMeshProUGUI score = null;
        [SerializeField] TextMeshProUGUI streak = null;

        [SerializeField] TextMeshProUGUI multiplier = null;
        
        [SerializeField] TextMeshProUGUI complete = null;
        

        GameObject player;
        ScoreKeeper scoreKeeper;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            scoreKeeper = player.GetComponent<ScoreKeeper>();
            complete.enabled = false;

            score.text = scoreKeeper.GetScore().ToString();
        }

        // Update is called once per frame
        void Update()
        {
            score.text = scoreKeeper.GetScore().ToString();
            streak.text = scoreKeeper.GetStreak().ToString();
            multiplier.text = scoreKeeper.GetMultiplier().ToString();
        }

        public void CompleteLevel()
        {
            complete.enabled = true;
        }

        public void StageLevel()
        {
            complete.enabled = false;
        }

        public void HalfWayBonus()
        {
            
        }
    }
}

