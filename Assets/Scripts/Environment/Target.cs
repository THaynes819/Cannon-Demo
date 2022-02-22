using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Demo.Control;
using Demo.Effects;

namespace Demo.Environment
{
    public class Target : MonoBehaviour
    {
        [SerializeField] int pointValue = 100;
        [SerializeField] TMP_Text pointDisplay = null;
        [SerializeField] AudioClip destroySound = null;
        [SerializeField] float destroyVolume = 0.8f;
        [Range(0.01f, 0.5f)][SerializeField] float multiplierPitchIncrease = 0.1f;
        private GameObject player;
        private ScoreKeeper scoreKeeper;
        GameBoard gameBoard;

        void Start()
        {
            pointDisplay.text = pointValue.ToString();
            player = GameObject.FindGameObjectWithTag("Player");
            scoreKeeper = player.GetComponent<ScoreKeeper>();
            gameBoard = GetComponentInParent<GameBoard>();
        }

        private void OnCollisionEnter(Collision other) 
        {
            scoreKeeper.IncreaseScore(pointValue);
            gameBoard.RemoveTarget(1);
            
            float multiplier = (float)scoreKeeper.GetMultiplier();
            float pitch = 1.0f;
            if (multiplier > 1)
            {
                float pitchChange =  1 + (multiplier * multiplierPitchIncrease);
                pitch = 1.0f * pitchChange;
            }
            else
            {
                pitch = 1.0f;
            }

            AudioManager.Instance.Play(destroySound, player.transform, destroyVolume, pitch);
        }
    }
}
