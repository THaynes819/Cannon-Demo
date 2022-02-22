using System.Collections;
using System.Collections.Generic;
using Demo.Control;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class ScoreUI : MonoBehaviour, ILevelCompleter
    {
        [SerializeField] TextMeshProUGUI score = null;
        [SerializeField] GameObject streak = null;
        [SerializeField] TextMeshProUGUI complete = null;

        [SerializeField] float streakDisplayTime = 2f;

        GameObject player;
        ScoreKeeper scoreKeeper;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            scoreKeeper = player.GetComponent<ScoreKeeper>();
            complete.enabled = false;
            streak.SetActive(false);
            score.text = scoreKeeper.GetScore().ToString();
        }

        // Update is called once per frame
        void Update()
        {
            score.text = scoreKeeper.GetScore().ToString();
            
            //multiplierValue.text = scoreKeeper.GetMultiplier().ToString();
        }

        public void CompleteLevel()
        {
            //Debug.Log("Complete should pop");
            complete.enabled = true;
        }

        public void StageLevel()
        {
            complete.enabled = false;
        }

        public void HalfWayBonus()
        {
            streak.SetActive(true);
            StartCoroutine(StreakBonus());
        }
        
        private IEnumerator StreakBonus()
        {
            //Debug.Log("Score UI setting Bonus Active"); Working 2/11/2022
            yield return new WaitForEndOfFrame();
            StartCoroutine(DelayedHide());
        }

        private IEnumerator DelayedHide()
        {
            yield return new WaitWhile(()=>scoreKeeper.GetHasBonus());
            if (streak.activeSelf)
            {
                streak.SetActive(false); 
            }
        }
    }
}

