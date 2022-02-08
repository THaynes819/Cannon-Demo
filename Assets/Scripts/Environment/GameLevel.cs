using System;
using System.Collections;
using System.Collections.Generic;
using Demo.Effects;
using Demo.PlayerSpace;
using UnityEngine;


namespace Demo.Environment
{
    public class GameLevel : MonoBehaviour, ILevelCompleter
    {
        [SerializeField] int levelNumber = 1;
        [SerializeField] AudioClip levelMusic = null;
        [SerializeField] AudioClip victorySound = null;
        [SerializeField] float victoryVolume = 0.1f;
        [Range(0.1f,5f)][SerializeField] float fadeSpeed = 1f;
        [SerializeField] float correctSmooth = 5.0f;
        [Range(0f, 100f)][SerializeField] int maxVolume =  50;
        [SerializeField] float startingVolume = 0.1f;
        public float currentVolume = 0.0f; //current volume is for monitoring and for the algorithm to evaluate to decide whether to continue
        private bool hasStarted = false;
        private bool hasFaded = false;
        private float fade;

        void Awake()
        {
            currentVolume = startingVolume;
            fade = fadeSpeed * 0.1f;
            if (startingVolume < maxVolume)
            {
                StartCoroutine(FadeIn());
            }
        }

        //Fades the Level Music in
        IEnumerator FadeIn()
        {
            int adjVolume = 0;
            if (!hasStarted)
            {
                adjVolume = (int)startingVolume * 100;
            }
            else
            {
                adjVolume = (int)currentVolume * 100;
            }
            
            while (currentVolume < maxVolume)
            {
                for (int i = adjVolume; i < maxVolume + 1; i++)
                {
                    yield return new WaitForSeconds(fade);
                    //Starts Music if it hasn'e started
                    if(!hasStarted)
                    {
                        AudioManager.Instance.PlayLoop(levelMusic, transform, startingVolume, 1f, true); 
                        hasStarted = true;
                    }
                    
                    if (hasStarted && !hasFaded)
                    {
                        if (adjVolume == maxVolume - 1)
                        {
                            hasFaded = true;
                        }
                    }
                    

                    //Inrememnts the volume to i every 0.1 seconds
                    AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Music, i);
                    adjVolume = i;
                    currentVolume = adjVolume;
                }
            }

            
            
        }

        void Update()
        {
            // Uodate only continues after hasFaded is true
            if (!hasFaded) return;
            int adjVolume = (int)currentVolume;
            
            if (hasFaded && currentVolume < maxVolume)
            {
                Debug.Log("Current Below Max after fade");
                currentVolume = Mathf.Lerp(currentVolume, maxVolume, correctSmooth * Time.deltaTime);
                AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Music, maxVolume);
            }
            
            if (currentVolume > maxVolume)
            {
                Debug.Log("Lowering Volume");
                currentVolume = maxVolume;
                
                Debug.Log("The adjusted current Volume is " + adjVolume);
                AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Music, adjVolume);
            }
        }

        public void CompleteLevel()
        {
            Debug.Log("Game Level complete");
            AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Music, 0);
            AudioManager.Instance.Play(victorySound, transform, victoryVolume);
        }

        public void StageLevel()
        {
            
        }
    }
}
