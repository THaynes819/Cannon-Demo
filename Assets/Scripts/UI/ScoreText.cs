using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Demo.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText = null;

        public void DestroyText ()
        {
            Destroy (gameObject);
        }

        public void SetValue (int amount, Color color)
        {
            scoreText.text = string.Format ("{0:0}", amount);
            scoreText.color = color;
            StartCoroutine(DelayedDestroyText());
        }

        IEnumerator DelayedDestroyText()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy (gameObject);
        } 
    }
}