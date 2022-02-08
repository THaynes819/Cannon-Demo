using System.Collections;
using System.Collections.Generic;
using Demo.PlayerSpace;
using UnityEngine;


namespace Demo.UI
{
    public class HudUI : MonoBehaviour
    {

    [SerializeField] RectTransform timerBackground = null;
    //[SerializeField] TextMeshProUGUI timer = null;

    GameObject player;
    ScoreKeeper scoreKeeper;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreKeeper = player.GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        timerBackground.localScale = new Vector3 (1, scoreKeeper.GetTimerFraction(), 1);
            // if (Mathf.Approximately(scoreKeeper.GetMultiplierTimer(), 0))
            // {
            //     timer.enabled = false;
            // }            
            // else
            // {
            //     timer.enabled = true;
            //     //timer.text = scoreKeeper.GetMultiplierTimer().ToString("0.0");
            // }
            
    }
    }
}
