using System.Collections;
using System.Collections.Generic;
using Demo.PlayerSpace;
using UnityEngine;


namespace Demo.UI
{
    public class HudUI : MonoBehaviour
    {
    [SerializeField] RectTransform[] multiplierFields = null;
    [SerializeField] RectTransform timerBackground = null;
    [SerializeField] RectTransform shootTimer = null;

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
            }
        }
        timerBackground.localScale = new Vector3 (1, _scoreKeeper.GetTimerFraction(), 1);
        
        shootTimer.localScale = new Vector3 (_shooter.GetShootFraction(),1,1);
    }
    }
}
