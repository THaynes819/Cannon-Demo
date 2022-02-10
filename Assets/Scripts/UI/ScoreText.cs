using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Demo.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] bool doesDestroySelf = true;
        [SerializeField] float lifeSpan = 1.0f;
        [SerializeField] Color textColor;
        [SerializeField] TextMeshProUGUI scoreText = null;
        private int _value;
        private Color _color;
        private Animator animator;

        private void OnEnable() 
        {
            string tempValue = _value.ToString();
            tempValue = string.Format ("{0:0}", tempValue);
            animator = GetComponent<Animator>();
            //scoreText.color = textColor;
        }
        public void DestroyText ()
        {
            Destroy (gameObject);
        }

        public void SetValue (int amount)
        {
            
            _value = amount;
            SpawnText();
                
                
                //string value = string.Format ("{0:0}", _value);
                
                //scoreTextInstance.color = textColor;
                
            
                // TextMeshProUGUI twoXScoreTextInstance = Instantiate(twoXScoreText, transform);
                // twoXScoreTextInstance.color = _xTwoColor;
                // twoXScoreTextInstance.text = string.Format ("{0:0}", _value);


            

            //Debug.Log("floating text being set. Color = " + _color);
            
            
        }

        public void SpawnText()
        {
            //TextMeshProUGUI scoreTextInstance = Instantiate(scoreText, transform);
            
            scoreText.text = string.Format ("{0:0}", _value);
            animator.SetTrigger("SpawnPoints");
            
            StartCoroutine(DelayedDestroyText());

        }

        private void Update() 
        {
            string tempValue = _value.ToString();
            tempValue = string.Format ("{0:0}", tempValue);
            
        }

        IEnumerator DelayedDestroyText()
        {
            yield return new WaitForSeconds(lifeSpan);

            if (doesDestroySelf)
            {
                Destroy (gameObject);
            }

        } 
    }
}