using System.Collections;
using System.Collections.Generic;
using Demo.UI;
using UnityEngine;

namespace Demo.UI
{
    public class UIPanelFader : MonoBehaviour
    {
        [SerializeField] float scoreFadeTime = 1.5f;
        private Coroutine _currentActiveFade = null;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        public void ShowScores()
        {
            StartCoroutine(FadeInPanel());
        }

        public void FadeScore()
        {
            StartCoroutine(FadeOutPanel());
        }

        public void FadeInOutScore()
        {
            StartCoroutine(FadeInOutPanel());
        }

        private IEnumerator FadeInPanel()
        {
            return Fade (1, scoreFadeTime);
            
        }

        private IEnumerator FadeOutPanel()
        {
            return Fade (0, scoreFadeTime);
        }

        private IEnumerator FadeInOutPanel()
        {
            yield return Fade (1, scoreFadeTime);
            Debug.Log("High Score Panel!");
            yield return new WaitForSeconds(scoreFadeTime);
            yield return Fade (0, scoreFadeTime);
        }

        private IEnumerator Fade(float target, float time)
        {
            
            if (_currentActiveFade != null)
            {
                StopCoroutine (_currentActiveFade);
            }
            _currentActiveFade = StartCoroutine (FadePanel(target, time));
            yield return _currentActiveFade;
        }

        private IEnumerator FadePanel(float target, float time)
        {
            while (!Mathf.Approximately (_canvasGroup.alpha, target))
            {
                _canvasGroup.alpha = Mathf.MoveTowards (_canvasGroup.alpha, target, Time.unscaledDeltaTime / time);
                yield return null;
            }
        }
    }
}
