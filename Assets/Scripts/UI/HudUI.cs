using System.Collections;
using System.Collections.Generic;
using Demo.Control;
using TMPro;
using UnityEngine;


namespace Demo.UI
{
    public class HudUI : MonoBehaviour, ILevelCompleter
    {
        [SerializeField] RectTransform[] multiplierFields = null;
        [SerializeField] RectTransform timerBackground = null;
        [SerializeField] RectTransform shootTimer = null;
        
        [SerializeField] TextMeshProUGUI multiplierText = null;
        [SerializeField] GameObject highScorePanel = null;
        private GameObject _player;
        private ScoreKeeper _scoreKeeper;
        private Shooter _shooter;

            

            void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _scoreKeeper = _player.GetComponent<ScoreKeeper>();
            _shooter = _player.GetComponent<Shooter>();



        }

        // Update is called once per frame
        void Update()
        {
            if (_scoreKeeper.GetMultiplier() <= 1)
            {
                foreach (var multiplier in multiplierFields)
                {
                    multiplier.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (var multiplier in multiplierFields)
                {
                    multiplier.gameObject.SetActive(true);
                    multiplierText.text = _scoreKeeper.GetMultiplier().ToString();
                }
            }
            timerBackground.localScale = new Vector3 (_scoreKeeper.GetTimerFraction(), 1, 1);
            
            shootTimer.localScale = new Vector3 (_shooter.GetShootFraction(),1,1);
        }
        public void CompleteLevel()
        {
            //Debug.Log("Setting High Score Panel Active");
            //highScorePanel.
        }

        public void HalfWayBonus()
        {
            
        }

        public void StageLevel()
        {
            
        }
    }
}
