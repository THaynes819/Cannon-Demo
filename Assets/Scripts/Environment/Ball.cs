using System;
using System.Collections;
using System.Collections.Generic;
using Demo.PlayerSpace;
using UnityEngine;

namespace Demo.Environment
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] float lifeSpan = 10.0f;
        private ScoreKeeper scoreKeeper;
        private GameObject player;
        private GameObject missedHit;

        private void OnEnable() 
        {
            player = GameObject.FindGameObjectWithTag("Player");
            scoreKeeper = player.GetComponent<ScoreKeeper>();
            missedHit = GameObject.FindGameObjectWithTag("Background");
        }

        void Update()
        {
            lifeSpan -= Time.deltaTime;
            
            if (lifeSpan <= 0)
            {
                KillBall();
            }
        }

        private void OnCollisionEnter(Collision other) 
        {
            if (other.gameObject.GetComponent<Target>())
            {
                //TODO Trigger an animation
                scoreKeeper.SetStreak(1);
                Destroy(other.gameObject);
                KillBall();
                return;
            }
            else
            {
                Debug.Log("Ball hit " + other.gameObject.name);
                scoreKeeper.BreakStreak();
                StartCoroutine(DelayedDestroy());
            }
        }



        IEnumerator DelayedDestroy()
        {
            yield return new WaitForSeconds(1.0f);

            KillBall();
        }

        public void KillBall()
        {
            Destroy(this.gameObject);
        }
    }
}
